using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto.Element;
using CardsServer.BLL.Entity;
using CardsServer.BLL.Infrastructure.Result;
using CardsServer.DAL.Repository;
using System.Collections;

namespace CardsServer.BLL.Services
{
    public sealed class ElementService : IElementService
    {
        private readonly IElementRepository _iElementRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IModuleRepository _moduleRepository;

        public ElementService(
            IImageRepository imageRepository, 
            IElementRepository iElementRepository, 
            IModuleRepository moduleRepository)
        {
            _imageRepository = imageRepository;
            _iElementRepository = iElementRepository;
            _moduleRepository = moduleRepository;
        }

        public async Task<Result> AddElement(AddElementModel model, int userId, CancellationToken cancellationToken)
        {
            ModuleEntity? module = await _moduleRepository.GetModule(model.ModuleId, cancellationToken);
            if(module == null) 
                return Result.Failure("Не найден модуль, для которого создается элемент!");
            if (module.Creator!.Id != userId)
                return Result.Failure(ErrorAdditional.Forbidden);
            ElementEntity newElement = new()
            {
                Key = model.Key,
                Value = model.Value,
                Module = module,
            };
            try
            {
                await _iElementRepository.AddElement(newElement, cancellationToken);
                return Result.Success();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Result> DeleteElementById(int id, int userId, CancellationToken cancellationToken)
        {
            Result<ElementEntity> check = await CheckElementAvailabityAndUserCorrect(id, userId, cancellationToken);
            if (check.IsSuccess)
            {
                try
                {
                    await _iElementRepository.DeleteElementById(check.Value, cancellationToken);
                    return Result.Success();
                }
                catch (Exception)
                {
                    throw; 
                }
            }
            return Result.Failure(check.Error);
        }

        public Task<Result> DeleteElements(int[] ids, int userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> EditElement(EditElementModel model, int userId, CancellationToken cancellationToken)
        {
            ModuleEntity? module = await _moduleRepository.GetModule(model.ModuleId, cancellationToken);
            if (module == null)
                return Result.Failure("Не найден модуль, для которого редактируется элемент!");
            if (module.Creator!.Id != userId)
                return Result.Failure(ErrorAdditional.Forbidden);
            ElementEntity element = await _iElementRepository.GetElement(model.ElementId, cancellationToken);
            if (element == null)
            {
                return Result.Failure("Редактируемый элемент не найден");
            }

            element.Value = model.Value;
            element.Key = model.Key;

            try
            {
                await _iElementRepository.EditElement(element, cancellationToken);
                return Result.Success();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetElement?> GetElement(int moduleId, CancellationToken cancellationToken)
        {
            ElementEntity? el = await _iElementRepository.GetElementByModuleId(moduleId, cancellationToken);
            if (el != null)
            {
                return new GetElement()
                {
                    Key = el.Key,
                    Value = el.Value,
                    //Image = await _imageRepository.GetImage()
                };
            }
            return null;
        }

        public async Task<Result<GetElement>> GetElementById(int id, int userId, CancellationToken cancellationToken)
        {
            ElementEntity? element = await _iElementRepository.GetElementCreatorId(x => x.Id == id, cancellationToken);
            if (element != null && element.Module!.Creator!.Id == userId)
            {
                GetElement result = new()
                {
                    Key = element.Key,
                    Value = element.Value,
                };
                return Result<GetElement>.Success(result);
            }

            return Result<GetElement>.Failure("Не удалось получить элемент!");
        }

        public async Task<Result<IEnumerable<GetElement>>> GetElementsByModuleId(int moduleId, CancellationToken cancellationToken)
        {
            IEnumerable<ElementEntity> elements =
                await _iElementRepository.GetElementsByModuleId(moduleId, cancellationToken);

            return Result<IEnumerable<GetElement>>.Success(elements.Select(x => new GetElement()
            {
                Key = x.Key, Value = x.Value, Id = x.Id,
                // TODO
                // Image = x.Image.,
            }));
        }


        private async Task<Result<ElementEntity>> CheckElementAvailabityAndUserCorrect(int elementId, int userId, CancellationToken cancellationToken)
        {
            ElementEntity? element = await _iElementRepository.GetElementCreatorId(x => x.Id == elementId, cancellationToken);
            if (element != null && element.Module!.Creator!.Id == userId)
            {
                return Result<ElementEntity>.Success(element);
            }
            else if (element == null)
            {
                return Result<ElementEntity>.Failure(ErrorAdditional.NotFound);
            }
            else if(element.Module!.CreatorId != userId)
            {
                return Result<ElementEntity>.Failure(ErrorAdditional.Forbidden);
            }
            return Result<ElementEntity>.Failure("Не удалось получить модуль");
        }
        //public async Task<GetElement> GetElement()
        //{

        //}
    }
}
