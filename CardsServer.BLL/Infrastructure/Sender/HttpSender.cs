using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CardsServer.BLL.Infrastructure.Sender
{
    public class HttpSender : IHttpSender
    {
        private readonly IConfiguration _configuration;

        public HttpSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        static HttpClient httpClient = new HttpClient();
        public async Task SendAsync(string Url, HttpMethods method, CancellationToken cancellationToken = default)
        {
            //using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Url);
            //using HttpResponseMessage response = await httpClient.SendAsync(request); 

            //using HttpResponseMessage response = await httpClient.GetAsync(Url, cancellationToken);

            //string response = await httpClient.GetStringAsync(Url,cancellationToken);

            httpClient.DefaultRequestHeaders.Authorization.

            await 
        }
        public async Task<T> SendAsync<T>(string Url, HttpMethods method, CancellationToken cancellationToken = default)
        {

            //var data = await httpClient.GetFromJsonAsync<T>(Url);

            using var response = await httpClient.GetAsync(Url, cancellationToken);

            if (response.StatusCode == Http)
            {
                
            }

            return;
        }

        private static Preparator(string Url, EnumServices service, HttpMethods method, CancellationToken cancellationToken = default)
        {

        }

        private string ServiceCollector(EnumServices enumServices)
        {
            switch (enumServices)
            {
                case EnumServices.EmailService:
                    return _configuration["Kafka:EmailService"];
                default:
                    throw new Exception("Сервис не найден!");
            }
        }

        private static HttpMethod CheckHttpMethod(HttpMethods methods)
        {
            switch (methods) 
            { 
                case HttpMethods.POST:
                    return HttpMethod.Post;
                case HttpMethods.GET:
                    return HttpMethod.Get;
                case HttpMethods.PUT:
                    return HttpMethod.Put;
                case HttpMethods.DELETE:
                    return HttpMethod.Delete;
                case HttpMethods.PATCH:
                    return HttpMethod.Patch;
                default:
                    throw new NotImplementedException("Метод не поддерживается");
            }
        }
    }
}
