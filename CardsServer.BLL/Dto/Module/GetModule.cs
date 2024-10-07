using CardsServer.BLL.Dto.Element;
using CardsServer.BLL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
