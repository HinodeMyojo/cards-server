using CardsServer.BLL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsServer.BLL.Dto.User
{
    public class PatchUser
    {
        /// <summary>
        /// Юзернейм
        /// </summary>
        public required string UserName { get; set; }
        /// <summary>
        /// Почта пользователя
        /// </summary>
        public required string Email { get; set; }
        public required string Password { get; set; }
        public AvatarEntity? Avatar { get; set; }
    }
}
