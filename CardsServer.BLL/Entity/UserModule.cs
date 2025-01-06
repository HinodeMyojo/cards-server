namespace CardsServer.BLL.Entity
{
    /// <summary>
    /// Промежуточная таблицы для связи м:м UserEntity и ModuleEntity
    /// </summary>
    public class UserModule
    {
        public int ModuleId { get; set; }
        public ModuleEntity? Module { get; set; }
        public int UserId { get; set; }
        public UserEntity? User { get; set; }
        public DateTime AddedAt { get; set; }
        /// <summary>
        /// Если true - то данный модуль приватный для выбранного пользователя! (А сам модуль может быть публичным)
        /// </summary>
        public bool IsPrivateForMe { get; set; }

    }
}
