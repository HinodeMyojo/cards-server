using System.Text.Json.Serialization;

namespace CardsServer.BLL.Dto.Module
{
    public class CreateModule : CreateEditModuleBase
    {
        [JsonIgnore]
        public DateTime CreateAt { get; set; }   
    }
}
