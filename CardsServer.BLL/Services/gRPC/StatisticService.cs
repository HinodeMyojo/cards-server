using Grpc.Core;
using StatisticService.API;

namespace CardsServer.BLL.Services.gRPC
{
    /// <summary>
    /// Сервис для связи с gRPC микросервисом
    /// </summary>
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

        public override AsyncUnaryCall<StatisticResponse> SaveStatisticAsync(StatisticRequest request, CallOptions options)
        {
            return _grpcChannel.SaveStatisticAsync(request, options);
        }

        // TODO
        public override AsyncUnaryCall<GetAwailableYearsResponse> GetAwailableYearsAsync(GetAwailableYearsRequest request, CallOptions options)
        {
            return base.GetAwailableYearsAsync(request, options);
        }

        // TODO
        public override AsyncUnaryCall<PingResponse> PingAsync(PingRequest request, CallOptions options)
        {
            return base.PingAsync(request, options);
        }
    }
}
