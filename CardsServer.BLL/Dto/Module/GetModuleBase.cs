using CardsServer.BLL.Dto.Element;

namespace CardsServer.BLL.Dto.Module
{
    public abstract class GetModuleBase
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreateAt { get; set; }
        /// <summary>
        /// Когда пользователь добавил модуль. 
        /// В случае если пользователь создатель - идентично с CreatedAt
        /// </summary>
        public DateTime AddedAt {  get; set; }
        public DateTime? UpdateAt { get; set; }
        public bool IsDraft { get; set; }
        public int CreatorId { get; set; }
        public List<GetElement> Elements { get; set; } = [];
    }
}