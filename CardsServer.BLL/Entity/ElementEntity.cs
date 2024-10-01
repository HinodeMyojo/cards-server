namespace CardsServer.BLL.Entity
{
    public class ElementEntity
    {
        public int Id { get; set; }
        public required string Key {  get; set; }
        public required string Value { get; set; }
        public int ModuleId { get; set; }
        public int? ImageId { get; set; }
        public ImageEntity? Image { get; set; }
    }
}
