
namespace CardsServer.BLL.Abstractions
{
    public interface IAnalyticService
    {
        Task<AnalyticsResponse> SendTestDataAsync(AnalyticsRequest request);
    }
}