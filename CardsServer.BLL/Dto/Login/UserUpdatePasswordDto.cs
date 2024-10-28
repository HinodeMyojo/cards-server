namespace CardsServer.BLL.Dto.Login
{
    public class UserUpdatePasswordDto
    {
        public required string Email { get; set; }
        public int Code { get; set; }
        public required string Password { get; set; }
        public required string SubmitPassword { get; set; }
    }
}
