using System.ComponentModel.DataAnnotations;

namespace CardsServer.BLL.Entity
{
    public class ElementEntity
    {
        [Key]
        public int Id { get; set; }
        public required string Key {  get; set; }
        public required string Value { get; set; }
        public int ModuleId { get; set; }
        public ModuleEntity? Module { get; set; }
        // Связь 1:1 с изображением
        public ElementImageEntity? Image { get; set; }
    }
}
