using CardsServer.BLL.Dto.Element;
using CardsServer.BLL.Entity;
using CardsServer.BLL.Infrastructure.Result;
using Microsoft.IdentityModel.Tokens;

namespace CardsServer.BLL.Dto.Module
{
    public class GetModule
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

        public static explicit operator GetModule(ModuleEntity res)
        {
            GetModule result = new()
            {
                Id = res.Id,
                Title = res.Title,
                CreateAt = res.CreateAt,
                Description = res.Description,
                CreatorId = res.CreatorId,
                IsDraft = res.IsDraft,
                UpdateAt = res.UpdateAt,
            };
            if (!res.Elements.IsNullOrEmpty())
            {
                foreach (ElementEntity item in res.Elements)
                {
                    result.Elements.Add(new GetElement()
                    {
                        Id = item.Id,
                        Key = item.Key,
                        Value = item.Value,
                        Image = item.Image != null ? Convert.ToBase64String(item.Image.Data) : null,
                    });
                }
            }

            return result;
        }

    }
}
