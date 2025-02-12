using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto.Element;
using CardsServer.BLL.Dto.Module;
using CardsServer.BLL.Entity;
using CardsServer.BLL.Enums;
using CardsServer.BLL.Infrastructure;
using CardsServer.BLL.Infrastructure.CustomExceptions;
using CardsServer.BLL.Infrastructure.Result;
using CardsServer.DAL.Repository;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace CardsServer.BLL.Services.Module
{
    public sealed class ModuleService : IModuleService
    {
        private readonly IElementRepostory _elementRepostory;
        private readonly IModuleRepository _moduleRepository;
        private readonly IImageService _imageService;
        private readonly IUserRepository _userRepository;
        private readonly IValidatorFactory _validatorFactory;
        private IValidator _validator;
        

        public ModuleService(
            IModuleRepository moduleRepository,
            IUserRepository userRepository,
            IImageService imageService,
            IElementRepostory elementRepostory, 
            IValidatorFactory validatorFactory)
        {
            _moduleRepository = moduleRepository;
            _userRepository = userRepository;
            _imageService = imageService;
            _elementRepostory = elementRepostory;
            _validatorFactory = validatorFactory;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="moduleForEdit"></param>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Result> EditModule(EditModule moduleForEdit, int userId, CancellationToken cancellationToken)
        { 
            try
            {
                UserEntity? user = await _userRepository.GetUser(x => x.Id == userId, cancellationToken);
                if (user == null)
                {
                    return Result<int>.Failure("User not found!");
                }
                ModuleEntity? moduleFromDb = await _moduleRepository.GetModule(moduleForEdit.Id, cancellationToken);

                if (moduleFromDb == null)
                {
                    return Result.Failure("Module not found!");
                }

                ModuleEntity? module = await _moduleRepository.GetModule(moduleForEdit.Id, cancellationToken);

                if (module == null)
                {
                    return Result<int>.Failure("Module not found.");
                }

                // TODO вынести в отдельный класс

                ValidateModesEnum mode;

                IEnumerable<PermissionEntity> userPermissions = await _userRepository.GetUserPermissions(userId, cancellationToken);
                if (userPermissions.Any(x => x.Id == (int)PermissionEnum.CanEditAnyModule))
                {
                    mode = ValidateModesEnum.EditModuleByAdmin;
                }
                else if(userPermissions.Any(x => x.Id == (int)PermissionEnum.CanEditOwnModule && module.CreatorId == userId))
                {
                    mode = ValidateModesEnum.EditModuleByUser;
                }
                else
                {
                    throw new PermissionNotFoundException("Permission not found!");
                }

                // Создаём валидатор
                IValidator factory = _validatorFactory.CreateValidator(mode);
                Result<string> result = factory.Validate(module);

                if (!result.IsSuccess)
                {
                    return result;
                }


                moduleFromDb.Title = module.Title;
                moduleFromDb.Description = module.Description;
                moduleFromDb.IsDraft = module.IsDraft;
                moduleFromDb.IsDraft = module.Private;
                // TODO
                //moduleFromDb.Elements


                await _moduleRepository.EditModule(moduleFromDb, cancellationToken);

                return Result.Success("Module updated successfully.");
            }
            catch
            {
                throw;
            }
        }

        
        public async Task<Result<int>> CreateModule(int userId, CreateModule module, CancellationToken cancellationToken)
        {
            //(bool isValid, string errorMessage) check = CheckTitle(module.Title);

            //if (!check.isValid)
            //    return Result<int>.Failure(check.errorMessage);

            try
            {

                UserEntity? user = await _userRepository.GetUser(x => x.Id == userId, cancellationToken);
                if (user == null)
                {
                    return Result<int>.Failure("User not found.");
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

                int moduleId = await _moduleRepository.CreateModule(entity, cancellationToken);
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
                ICollection<ModuleEntity> listOfOriginModules = await _moduleRepository.GetModules(
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

        public async Task<Result<IEnumerable<GetModule>>> GetUsedModules(int userId, string? textSearch, CancellationToken cancellationToken)
        {
            try
            {
                ICollection<ModuleEntity> listOfOriginModules;

                if (textSearch == null)
                {
                    listOfOriginModules = await _moduleRepository.GetModules(
                    userId,
                    x => x.UsedUsers.Any(x => x.Id == userId),
                    cancellationToken);
                }
                else
                {
                    listOfOriginModules = await _moduleRepository.GetModules(
                    userId,
                    x => x.UsedUsers.Any(x => x.Id == userId) && x.Title.StartsWith(textSearch),
                    cancellationToken);
                }

                ICollection<GetModule> result = [];
                if (listOfOriginModules.Count != 0)
                {
                    await ElementsHandler(listOfOriginModules, result, cancellationToken, userId);
                }

                List<GetModule> orderedResult;

                if (textSearch == null)
                {
                    orderedResult = [.. result.OrderBy(x => x.AddedAt)];
                }
                else
                {
                    orderedResult = [.. result.OrderBy(x => x.Title.StartsWith(textSearch))];
                }


                return Result<IEnumerable<GetModule>>.Success(orderedResult);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Result> AddModuleToUsed(int moduleId, int userId, CancellationToken cancellationToken)
        {
            ModuleEntity? module = await _moduleRepository.GetModule(moduleId, cancellationToken);
            UserEntity? user = await _userRepository.GetUser(x => x.Id == userId, cancellationToken);
            if (module == null || user == null)
            {
                return Result.Failure(ErrorAdditional.NotFound);
            }

            user.UserModules.Add(new UserModule() { Module = module });

            await _moduleRepository.AddModuleToUsed(user, cancellationToken);

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

            await _moduleRepository.DeleteModule(id, cancellationToken);

            return Result.Success();
        }

        public async Task<Result<IEnumerable<GetModule>>> GetModulesShortInfo(int[] moduleId, int userId, CancellationToken cancellationToken)
        {
            IEnumerable<ModuleEntity> resultFromDB = await _moduleRepository.GetModulesShortInfo(moduleId, cancellationToken);

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

        public async Task<Result<IEnumerable<GetModuleBase>>> GetModules(int userId, GetModulesRequest model, CancellationToken cancellationToken)
        {
            IEnumerable<ModuleEntity> responseFromDatabase = await _moduleRepository.GetModules(model, cancellationToken);

            IEnumerable<GetModuleBase> result;
            
            if (model.AddCreatorAvatar || model.AddCreatorUserName || model.AddCommentCount || model.AddTags)
            {
                result = responseFromDatabase.Select(x => (GetModuleExpanded)x);
                return Result<IEnumerable<GetModuleBase>>.Success(result);
            }
            
            result = responseFromDatabase.Select(x => (GetModule)x);

            return Result<IEnumerable<GetModuleBase>>.Success(result);

        }

        public async Task<Result<GetModule>> GetModule(int userId, int id, CancellationToken cancellationToken)
        {
            try
            {
                ModuleEntity? res = await _moduleRepository.GetModule(id, cancellationToken);

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
    }
}
