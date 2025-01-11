using System.ComponentModel.DataAnnotations.Schema;

namespace CardsServer.BLL.Entity
{
    public class ProfileEntity
    {
        public int Id { get; set; }
        public  UserEntity User { get; set; }
        public int UserId { get; set; }
        public bool IsPrivate { get; set; }
    }
}
