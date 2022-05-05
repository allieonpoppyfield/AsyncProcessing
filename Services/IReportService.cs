namespace AsyncProcessing.Services;

public interface IReportService
{
    public Task<UserStatisticsResponse> GetUserStatistics(UserStatisticsRequest request);
    public Task<QueryInfoResponse> GetQueryInfo(Guid queryId);
}
