namespace CardsServer.BLL.Dto.Statistic
{
    public class YearStatistic
    {
        public int Year {  get; set; }
        public List<YearStatisticData> Data { get; set; } = [];
    }

    public class YearStatisticDto
    {
        public int Year { get; set; }
        public int NumberOfActions  { get; set; }
        public int ActiveDays  { get; set; }
        public int MaximumSeries  { get; set; }
        public YearStatisticData[][] Data { get; set; } = [];
        public List<int> Colspan { get; set; } = [];
    }

    public class YearStatisticData
    {
        public DateTime Date {  get; set; }
        public int? Value {  get; set; }
    }
}
