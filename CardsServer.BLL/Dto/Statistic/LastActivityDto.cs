namespace CardsServer.BLL.Dto.Statistic
{
    public class LastActivityDTO
    {
        public ICollection<LastActivityModel> ActivityList { get; set; } = [];
    }
    public class LastActivityModel
    {
        public required string Name { get; set; }
        public DateTime AnsweredAt {  get; set; }
        public int Id { get; set; }
    }
}
