namespace CardsServer.BLL.Entity
{
    public class LogsEntity
    {
        public int Id { get; set; }
        public DateTime CreateAt {  get; set; }
        public required string State { get; set; }
        public required string Exception { get; set; }
    }
}
