using CardsServer.BLL.Dto.Element;
using CardsServer.BLL.Entity;
using Microsoft.IdentityModel.Tokens;

namespace CardsServer.BLL.Dto.Module
{
    public class GetModuleExpanded : GetModuleBase
    {
        public string? Avatar { get; set; }
        public string? CreatorUserName { get; set; }
        public int CommentCount { get; set; }
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
        public IEnumerable<string> Tags { get; set; } = [];

        public static explicit operator GetModuleExpanded(ModuleEntity res)
        {
            var result = new GetModuleExpanded
            {
                Id = res.Id,
                Title = res.Title,
                CreateAt = res.CreateAt,
                Description = res.Description,
                CreatorId = res.CreatorId,
                IsDraft = res.IsDraft,
                UpdateAt = res.UpdateAt,
                Avatar = res.Creator?.Avatar?.Data != null 
                    ? Convert.ToBase64String(res.Creator.Avatar.Data) 
                    : null,
                CreatorUserName = res.Creator?.UserName,
                // CommentCount = res.Comments?.Count ?? 0,
                // LikeCount = res.Likes?.Count(l => l.IsLike) ?? 0,
                // DislikeCount = res.Likes?.Count(l => !l.IsLike) ?? 0,
                // Tags = res.Tags?.Select(t => t.Name) ?? Enumerable.Empty<string>()
            };

            if (!res.Elements.IsNullOrEmpty())
            {
                result.Elements.AddRange(res.Elements.Select(item => 
                    CreateElementDto(item)));
            }

            return result;
        }

        private static GetElement CreateElementDto(ElementEntity item)
        {
            return new GetElement
            {
                Id = item.Id,
                Key = item.Key,
                Value = item.Value,
                Image = item.Image?.Data != null 
                    ? Convert.ToBase64String(item.Image.Data) 
                    : null
            };
        }
    }
}