namespace CardsServer.BLL.Dto.Statistic
{
    public class YearStatistic
    {
        public int Year {  get; set; }
        public List<YearStatisticData> Data { get; set; } = [];
    }

    public class YearStatisticData
    {
        public DateTime Date {  get; set; }
        public int? Value {  get; set; }
    }
}
