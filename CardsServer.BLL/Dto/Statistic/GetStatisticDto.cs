namespace CardsServer.BLL.Dto.Statistic
{
    public class GetStatisticDto
    {
        public int PercentSuccess { get; set; }
        public int NumberOfAttempts { get; set; }
        public DateTime CompletedAt { get; set; }
    }
}
