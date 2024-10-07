using CardsServer.BLL.Dto.Element;
using CardsServer.BLL.Entity;
using CardsServer.DAL.Repository;
namespace CardsServer.BLL.Services
{
    public class ElementService : IElementService
    {
        private readonly IElementRepostory _elementRepository;
        private readonly IImageRepository _imageRepository;

        public ElementService(IImageRepository imageRepository, IElementRepostory elementRepository)
        {
            _imageRepository = imageRepository;
            _elementRepository = elementRepository;
        }

        public async Task<GetElement?> GetElement(int moduleId, CancellationToken cancellationToken)
        {
            ElementEntity? el = await _elementRepository.GetElementByModuleId(moduleId, cancellationToken);
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

        //public async Task<GetElement> GetElement()
        //{

        //}
    }
}
