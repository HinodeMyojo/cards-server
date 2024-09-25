using CardsServer.BLL.Dto.Login;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace CardsServer.BLL.Infrastructure.RabbitMq
{
    public class RabbitMQPublisher : IRabbitMQPublisher
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQPublisher()
        {
            // Настройка подключения к RabbitMQ
            var factory = new ConnectionFactory() { HostName = "host.docker.internal", Port = 5672, UserName = "admin", Password = "admin" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            // Создание очереди 
            _channel.QueueDeclare(queue: "emailQueue",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);
        }

        //public void SendMessage(object obj)
        //{
        //    var message = JsonSerializer.Serialize(obj);
        //    SendMessage(message);
        //}

        public void SendEmail(SendMailDto model)
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));
            _channel.BasicPublish(exchange: "emailExchange",
                               routingKey: "email.send",
                               basicProperties: null,
                               body: body);
        }

        public async void SendEmailWithFiles(SendMailDto model, IFormFileCollection files)
        {
            ByteConverter converter = new();

            var emailData = new SendMailWithFilesDto 
            {
                Message = model, 
                Files = await converter.ConvertFilesToByteArrayAsync(files)
            }; // Объект с данными для отправки
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(emailData));

            // Отправляем в exchange с routing key "email.files"
            _channel.BasicPublish(exchange: "emailExchange",
                                   routingKey: "email.send.files",
                                   basicProperties: null,
                                   body: body);
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
