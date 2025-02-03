namespace CardsServer.BLL.Dto.Module
{
    public class EditModule : CreateEditModuleBase
    {
        [JsonIgnore]
        public DateTime UpdateAt { get; set; }
    }
}
