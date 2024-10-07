using CardsServer.BLL.Entity;
using Microsoft.AspNetCore.Http;
namespace CardsServer.BLL.Dto.Image
{
    public class CreateImage
    {
        public int UserId { get; set; }
        public UserEntity? User { get; set; }
        public required string Image { get; set; }
        
    }
}
