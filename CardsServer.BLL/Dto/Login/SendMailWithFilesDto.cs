using Microsoft.AspNetCore.Http;

namespace CardsServer.BLL.Dto.Login
{
    public class SendMailWithFilesDto
    {
        public required SendMailDto Message {  get; set; }
        public required ICollection<byte[]> Files {  get; set; }
    }
}
