namespace CardsServer.BLL.Dto.Card
{
    public class SaveModuleStatistic
    {
        public int UserId { get; set; }
        public int ModuleId { get; set; }
        public required List<SaveElementStatistic> ElementStatistics { get; set; }
        public DateTime CompletedAt { get; set; }
    }
}
