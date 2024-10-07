using Microsoft.AspNetCore.Http;
using System.Net.Http.Formatting;

namespace CardsServer.BLL.Dto.Element
{
    public class CreateElement
    {
        public required string Key { get; set; }
        public required string Value { get; set; }
        public string? Image { get; set; }
    }
}
