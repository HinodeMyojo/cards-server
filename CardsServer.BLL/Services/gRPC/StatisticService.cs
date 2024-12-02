using Grpc.Core;
using Grpc.Net.Client;
using StatisticService.API;

namespace CardsServer.BLL.Services.gRPC
{
    public class StatisticService : Statistic.StatisticClient
    {
        private readonly Statistic.StatisticClient _grpcChannel;

        public StatisticService(Statistic.StatisticClient grpcChannel)
        {
            _grpcChannel = grpcChannel;
        }

        public override AsyncUnaryCall<YearStatisticResponse> GetYearStatisicAsync(YearStatisticRequest request, CallOptions options)
        {
            return _grpcChannel.GetYearStatisicAsync(request, options);
        }
    }
}
