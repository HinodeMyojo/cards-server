namespace CardsServer.BLL.Dto.User
{
    public class GetUserFullResponse : GetUserSimpleResponse
    {
        public DateTime CreatedAt {  get; set; }
        public required string Email { get; set; }
    }
}
