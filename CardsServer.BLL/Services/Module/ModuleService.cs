using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto.Element;
using CardsServer.BLL.Dto.Module;
using CardsServer.BLL.Entity;
using CardsServer.BLL.Infrastructure.Result;
using CardsServer.DAL.Repository;

namespace CardsServer.BLL.Services.Module
{
    public class ModuleService : IModuleService
    {
        private readonly IElementRepostory _elementRepostory;
        private readonly IModuleRepository _repository;
        private readonly IImageService _imageService;
        private readonly IUserRepository _userRepository;

        public ModuleService(
            IModuleRepository repository, 
            IUserRepository userRepository, 
            IImageService imageService, 
            IElementRepostory elementRepostory)
        {
            _repository = repository;
            _userRepository = userRepository;
            _imageService = imageService;
            _elementRepostory = elementRepostory;
        }

        public async Task<Result<int>> CreateModule(int userId, CreateModule module, CancellationToken cancellationToken)
        {
            (bool isValid, string errorMessage) check = CheckTitle(module.Title);
            if (!check.isValid)
                return Result<int>.Failure(check.errorMessage);
            try
            {

                UserEntity? user = await _userRepository.GetUser(userId, cancellationToken);
                if (user == null)
                {
                    return Result<int>.Failure("Возникла ошибка при создании модуля. Не удалось идентифицировать пользователя!");
                }

                ModuleEntity entity = new()
                {
                    Title = module.Title,
                    CreateAt = module.CreateAt,
                    CreatorId = userId,
                    Description = module.Description,
                    IsDraft = module.IsDraft,
                    Private = module.Private,
                    //UsedUsers = [user],
                    // Так как мы вынесли в отдельную сущность нашу промежуточную таблицу
                    // То добавление пользователя вместе с временем просходит путем добавления в нашу промежуточную таблицу UserModules
                    UserModules = [new UserModule() { User = user, AddedAt = DateTime.UtcNow }]
                };

                if (module.Elements.Any())
                {
                    object locker = new();
                    Parallel.ForEach(module.Elements, (element, cancellationToken) =>
                    {
                        ElementEntity el = new()
                        {
                            Key = element.Key,
                            Value = element.Value
                        };

                        if (element.Image != null && element.Image != "")
                        {
                            el.Image = new ElementImageEntity()
                            {
                                //UserId = userId,
                                Data = Convert.FromBase64String(element.Image)
                            };
                        }

                        lock (locker) 
                        {
                            entity.Elements.Add(el);
                        }
                        
                    });
                }

                int moduleId = await _repository.CreateModule(entity, cancellationToken);
                return Result<int>.Success(moduleId);
            }
            catch (Exception ex)
            {
                return Result<int>.Failure(ErrorAdditional.BadRequest);
            }
        }

        public async Task<Result<IEnumerable<GetModule>>> GetCreatedModules(int userId, CancellationToken cancellationToken)
        {
            try
            {
                ICollection<ModuleEntity> listOfOriginModules = await _repository.GetModules(
                    userId,
                    x => x.CreatorId == userId,
                    cancellationToken);

                ICollection<GetModule> result = [];
                if (listOfOriginModules.Any())
                {
                    await ElementsHandler(listOfOriginModules, result, cancellationToken, userId);
                }

                result.OrderBy(x => x.CreateAt);

                return Result<IEnumerable<GetModule>>.Success(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Result<IEnumerable<GetModule>>> GetUsedModules(int userId, CancellationToken cancellationToken)
        {
            try
            {
                ICollection<ModuleEntity> listOfOriginModules = await _repository.GetModules(
                    userId,
                    x => x.UsedUsers.Any(x => x.Id == userId),
                    cancellationToken
                );

                ICollection<GetModule> result = [];
                if (listOfOriginModules.Count != 0)
                {
                    await ElementsHandler(listOfOriginModules, result, cancellationToken, userId);
                }
                List<GetModule> orderedResult = [.. result.OrderBy(x => x.AddedAt)];

                return Result<IEnumerable<GetModule>>.Success(orderedResult);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Result> AddModuleToUsed(int moduleId, int userId, CancellationToken cancellationToken)
        {
            ModuleEntity? module = await _repository.GetModule(moduleId, cancellationToken);
            UserEntity? user = await _userRepository.GetUser(userId, cancellationToken);
            if (module == null || user == null)
            {
                return Result.Failure(ErrorAdditional.NotFound);
            }

            user.UserModules.Add(new UserModule() { Module = module });

            await _repository.AddModuleToUsed(user, cancellationToken);

            return Result.Success();
        }

        public async Task<Result> DeleteModule(int userId, int id, CancellationToken cancellationToken)
        {
            Result<GetModule> module = await GetModule(userId, id, cancellationToken);
            if (!module.IsSuccess)
            {
                return Result.Failure("Возникла ошибка при получении модуля");
            }

            if (module.Value.CreatorId != userId)
            {
                return Result.Failure(ErrorAdditional.Forbidden);
            }

            await _repository.DeleteModule(id, cancellationToken);

            return Result.Success();
        }

        public async Task<Result<IEnumerable<GetModule>>> GetModulesShortInfo(int[] moduleId, int userId, CancellationToken cancellationToken)
        {
            IEnumerable<ModuleEntity> resultFromDB = await _repository.GetModulesShortInfo(moduleId, cancellationToken);

            IEnumerable<GetModule> result = resultFromDB.Select(x => new GetModule()
            {
                Id = x.Id,
                Title = x.Title,
                CreateAt = x.CreateAt,
                CreatorId = x.CreatorId,
                Description = x.Description,
                UpdateAt = x.UpdateAt
            });

            return Result<IEnumerable<GetModule>>.Success(result);
        }

        public async Task<Result<GetModule>> GetModule(int userId, int id, CancellationToken cancellationToken)
        {
            try
            {
                ModuleEntity? res = await _repository.GetModule(id, cancellationToken);

                if (res == null)
                {
                    return Result<GetModule>.Failure(ErrorAdditional.NotFound);
                }
                if (res.CreatorId != userId && res.Private)
                {
                    return Result<GetModule>.Failure(ErrorAdditional.Forbidden);
                }


                GetModule result = new()
                {
                    Id = id,
                    Title = res.Title,
                    CreateAt = res.CreateAt,
                    Description = res.Description,
                    CreatorId = res.CreatorId,
                    IsDraft = res.IsDraft,
                    UpdateAt = res.UpdateAt,
                };
                if (res.Elements.Any())
                {
                    foreach (ElementEntity item in res.Elements)
                    {
                        result.Elements.Add(new GetElement()
                        {
                            Id = item.Id,
                            Key = item.Key,
                            Value = item.Value,
                            Image = item.Image != null ? Convert.ToBase64String(item.Image.Data) : null,
                        });
                    }
                }


                return Result<GetModule>.Success(result);

            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        

        /// <summary>
        /// Метод для маппинга внутренних объектов Модуля
        /// </summary>
        /// <param name="listOfOriginModules"></param>
        /// <param name="resultList"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task ElementsHandler(
            ICollection<ModuleEntity> listOfOriginModules,
            ICollection<GetModule> resultList,
            CancellationToken cancellationToken,
            int userId)
        {
            var resultLock = new object();

            await Parallel.ForEachAsync(listOfOriginModules, cancellationToken, async (module, ct) =>
            {
                var moduleDto = new GetModule
                {
                    Id = module.Id,
                    Title = module.Title,
                    CreateAt = module.CreateAt,
                    CreatorId = module.CreatorId,
                    Description = module.Description,
                    IsDraft = module.IsDraft,
                    UpdateAt = module.UpdateAt,
                    AddedAt = module.UserModules
                        .Where( x => x.UserId == userId)
                        .Select( x => x.AddedAt)
                        .FirstOrDefault()

                };

                if (module.Elements.Count != 0)
                {
                    foreach (ElementEntity item in module.Elements)
                    {
                        moduleDto.Elements.Add(new GetElement()
                        {
                            Id = item.Id,
                            Key = item.Key,
                            Value = item.Value,
                            Image = item.Image != null ? Convert.ToBase64String(item.Image.Data) : null,
                        });
                    }
                }

                lock (resultLock)
                {
                    resultList.Add(moduleDto);
                }

                await Task.CompletedTask;
            });
        }

        private (bool isValid, string errorMessage) CheckTitle(string title, int minLength = 2, int maxLength = 256)
        {
            // Проверка на null или пустую строку
            if (string.IsNullOrWhiteSpace(title))
            {
                return (false, "Название не должно быть пустым или состоять только из пробелов.");
            }

            if (title.Length < minLength)
            {
                return (false, $"Название должно содержать не менее {minLength} символов.");
            }

            if (title.Length > maxLength)
            {
                return (false, $"Название должно содержать не более {maxLength} символов.");
            }

            //if (!title.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)))
            //{
            //    return (false, "Название содержит недопустимые символы.");
            //}

            return (true, "");
        }
    }
}
