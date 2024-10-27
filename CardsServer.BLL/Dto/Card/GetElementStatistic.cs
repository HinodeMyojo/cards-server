namespace CardsServer.BLL.Dto.Card
{
    public class GetElementStatistic
    {
        public int UserId { get; set; }
        public int ModuleId { get; set; }
        public int TrueAnswer {  get; set; }
        public int FalseAnswer { get; set; }
        public double TrueAnswerPersent { get; set; }
    }
}
