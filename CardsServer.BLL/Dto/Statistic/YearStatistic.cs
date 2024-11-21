namespace CardsServer.BLL.Dto.Statistic
{
    public class YearStatistic
    {
        public int Year {  get; set; }
        public List<YearStatisticMonthData> Data { get; set; } = [];
    }

    public class YearStatisticMonthData
    {
        public int Month {  get; set; }
        public List<YearStatisticDayData> Data { get; set; } = [];
    }

    public class YearStatisticDayData
    {
        public int Day { get; set; }
        public required int DayOfWeek {  get; set; }
        public List<YearStatisticData> Data { get; set; } = [];
    }

    public class YearStatisticData
    {
    }
}
