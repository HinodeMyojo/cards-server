using CardsServer.BLL.Dto.Image;

namespace CardsServer.BLL.Entity
{
    public class ElementImageEntity
    {
        public int Id { get; set; }
        public required byte[] Data { get; set; }

        // Связь 1:1 с элементом
        public int ElementId { get; set; }
        public ElementEntity? Element { get; set; }
    }
}
