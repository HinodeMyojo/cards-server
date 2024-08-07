using CardsServer.BLL.Infrastructure.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CardsServer.BLL.Dto
{
    public class RegisterUser
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        [PasswordValidation]
        public required string Password { get; set; }
    }
}
