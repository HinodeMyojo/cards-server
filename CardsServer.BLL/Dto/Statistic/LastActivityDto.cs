namespace CardsServer.BLL.Dto.Statistic
{
    public class LastActivityDTO
    {
        public ICollection<LastActivityModel> ActivityList { get; set; } = [];
    }
    public class LastActivityModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
