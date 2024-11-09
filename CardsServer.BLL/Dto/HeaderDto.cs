namespace CardsServer.BLL.Dto
{
    public class HeaderDto
    {
        public required string Title { get; set; }
        public bool Sortable { get; set;}
        public required string Key { get; set;}
    }
}
