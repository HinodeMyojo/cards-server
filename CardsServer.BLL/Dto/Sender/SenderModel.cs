namespace CardsServer.BLL.Dto.Sender
{
    public class SenderModel
    {
        public required Uri Url { get; set; }
        public required HttpMethod HttpMethod { get; set; }
        public object? Data { get; set; }
    }
}
