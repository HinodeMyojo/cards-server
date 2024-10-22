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

    }
}
