namespace CardsServer.BLL.Dto.Card
{
    public class SaveModuleStatistic
    {
        public int ModuleId { get; set; }
        public required List<SaveElementStatistic> ElementStatistics { get; set; }
    }
}
