using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto.Module;
using CardsServer.BLL.Entity;
using CardsServer.BLL.Infrastructure.Result;

namespace CardsServer.BLL.Services.Module
{
    public class ModuleService : IModuleService
    {
        private readonly IModuleRepository _repository;
        private readonly IUserRepository _userRepository;

        public ModuleService(IModuleRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task<Result<int>> CreateModule(
            int userId, CreateModule module, CancellationToken cancellationToken)
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
                    Elements = module.Elements,
                    IsDraft = module.IsDraft,
                    Private = module.Private,
                    UsedUsers = [user],
                };

                int result = await _repository.CreateModule(entity, cancellationToken);

                return Result<int>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<int>.Failure(ErrorAdditional.BadRequest);
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
