using CardsServer.BLL.Dto.Login;
using Microsoft.AspNetCore.Http;

namespace CardsServer.BLL.Infrastructure.RabbitMq
{
    public interface IRabbitMQPublisher
    {
        void SendEmail(SendMailDto model);
        void SendEmailWithFiles(SendMailDto model, IFormFileCollection files);
    }
}
