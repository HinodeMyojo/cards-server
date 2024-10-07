using System.ComponentModel.DataAnnotations;
namespace CardsServer.BLL.Dto.Element
{
    public class GetElement
    {
        [Key]
        public int Id { get; set; }
        public required string Key { get; set; }
        public required string Value { get; set; }
        public string? Image {  get; set; }
    }
}
