using CardsServer.BLL.Abstractions;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;

namespace CardsServer.BLL.Services.Analytic
{
    public class AnalyticService : IAnalyticService
    {
        //private readonly DataAnalytic.DataAnalyticClient _grpcClient;
        //private readonly GrpcChannel _grpcChannel;

        //public AnalyticService(GrpcChannel grpcChannel)
        //{
        //    _grpcChannel = grpcChannel;
        //}

        private readonly GrpcChannel _grpcChannel;

        public AnalyticService(GrpcChannel grpcChannel)
        {
            _grpcChannel = grpcChannel;
        }

        public async Task<AnalyticsResponse> SendTestDataAsync(AnalyticsRequest request)
        {
            //using var channel = GrpcChannel.ForAddress("http://analytic-service:8080");
            var client = new DataAnalytic.DataAnalyticClient(_grpcChannel);

            // Отправляем запрос
            AnalyticsResponse response = await client.GetAnalyticsAsync(request);

            // Выводим результат
            Console.WriteLine($"Result: {response.Result}");
            Console.WriteLine($"Details: {response.Details}");

            return response;
        }
    }
}
