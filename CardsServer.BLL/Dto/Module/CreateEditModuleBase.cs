using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto.Element;

namespace CardsServer.BLL.Dto.Module
{
    public class CreateEditModuleBase
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public bool Private { get; set; }
        public bool IsDraft { get; set; }
        public List<CreateElement> Elements { get; set; } = [];
    }
}
