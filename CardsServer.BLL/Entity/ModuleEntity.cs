namespace CardsServer.BLL.Entity
{
    public class ModuleEntity
    {
        public int Id { get; set; }
        public required string Title {  get; set; }
        public string? Description { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public bool Private {  get; set; }
        public bool IsDraft { get; set; }
        public int CreatorId { get; set; }
        public List<ElementEntity> Elements { get; set; } = [];
        public List<UserEntity> UsedUsers { get; set; } = [];
    }
}
