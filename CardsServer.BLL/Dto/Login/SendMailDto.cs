namespace CardsServer.BLL.Dto.Login
{
    public class SendMailDto
    {
        public List<string> To { get; set; } = [];
        public string? Subject { get; set; }
        public string? Content { get; set; }
    }
}
