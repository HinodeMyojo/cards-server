using CardsServer.BLL.Dto.Image;

namespace CardsServer.BLL.Entity
{
    public class ImageEntity
    {
        public ImageEntity()
        {

        }

        public ImageEntity(CreateImage create)
        {
            UserId = create.UserId;
            User = create.User;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public UserEntity? User { get; set; }
        public int ElementId { get; set; }
        public required byte[] Data { get; set; }
    }
}
