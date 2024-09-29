namespace CardsServer.BLL.Entity
{
    public class UserEntity
    {
        public int Id { get; set; }
        /// <summary>
        /// Юзернейм
        /// </summary>
        public required string UserName { get; set; }
        /// <summary>
        /// Почта пользователя
        /// </summary>
        public required string Email { get; set; }
        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public required string Password { get; set; }
        /// <summary>
        /// Подтвержденный ли емайл
        /// 0 - нет
        /// 1 - да
        /// </summary>
        public required bool IsEmailConfirmed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public required int StatusId { get; set; }
        public StatusEntity? Status { get; set; }
        /// <summary>
        /// Роль
        /// </summary>
        public required int RoleId { get; set; }
        public int RecoveryCode {  get; set; }
        public RoleEntity? Role { get; set; }
        public AvatarEntity? Avatar { get; set; }

    }
}
