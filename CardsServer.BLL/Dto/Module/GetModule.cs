using CardsServer.BLL.Dto.Element;

namespace CardsServer.BLL.Dto.Module
{
    public class GetModule
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public bool IsDraft { get; set; }
        public int CreatorId { get; set; }
        public List<GetElement> Elements { get; set; } = [];
    }
}
