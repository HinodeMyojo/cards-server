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
                    UsedUsers = [user],
                };

                //int moduleId = await _repository.CreateModule(entity, cancellationToken);

                if (module.Elements.Any())
                {
                    foreach (CreateElement element in module.Elements)
                    {
                        ElementEntity el = new()
                        {
                            Key = element.Key,
                            Value = element.Value,
                            //ModuleId = moduleId
                        };

                        //int elementId = await _elementRepostory.AddElement(el, cancellationToken);

                        if (element.Image != null)
                        {


                            //var res = await _imageService.UploadImage(
                            //    new CreateImage()
                            //    {
                            //        UserId = userId,
                            //        Image = element.Image
                            //    },
                            //    cancellationToken);

                            el.Image = new ImageEntity()
                            {
                                UserId = userId,
                                Data = Convert.FromBase64String(element.Image)
                            };
                        }

                        entity.Elements.Add(el);
                    }
                }

                int moduleId = await _repository.CreateModule(entity, cancellationToken);
                return Result<int>.Success(moduleId);
            }
            catch (Exception ex)
            {
                return Result<int>.Failure(ErrorAdditional.BadRequest);
            }
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

                //List<ElementEntity> elements = await _elementRepostory.GetElementsByModuleId(userId, cancellationToken);


                GetModule result = new()
                {
                    Title = res.Title,
                    CreateAt = res.CreateAt,
                    Description = res.Description,
                    CreatorId = res.CreatorId,
                    IsDraft = res.IsDraft,
                    UpdateAt = res.UpdateAt,
                    //Elements = res.Elements
                };

                return Result<GetModule>.Success(result);

            }
            catch (Exception ex)
            {
                throw new Exception();
            }
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
