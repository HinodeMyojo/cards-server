namespace CardsServer.BLL.Dto.Element
{
    public class AddElement
    {
        public required string Key { get; set; }
        public required string Value { get; set; }
        public int ModuleId {  get; set; }
    }
}
