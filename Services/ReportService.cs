namespace AsyncProcessing.Services;

public class ReportService : IReportService
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly ITaskService _taskService;


    public ReportService(ApplicationDbContext dbContext, IConfiguration configuration, IMapper mapper, ITaskService taskService)
    {
        _context = dbContext;
        _configuration = configuration;
        _mapper = mapper;
        _taskService = taskService;
    }

    public async Task<UserStatisticsResponse> GetUserStatistics(UserStatisticsRequest request)
    {
        var query = _mapper.Map<Query>(request);
        query.Id = Guid.NewGuid();
        query.CreatedDateUnixTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        await _context.Queries.AddAsync(query);
        await _context.SaveChangesAsync();
        Task insertQueryTask = Task.Run(async () =>
        {
            try
            {
                var currentTaskRunnigMilliseconds = (DateTimeOffset.Now.ToUnixTimeMilliseconds() - query.CreatedDateUnixTime);
                var delayMilliseconds = _configuration.GetValue<int>("DelayMillisecondsCount");
                if (currentTaskRunnigMilliseconds <= delayMilliseconds)
                    await Task.Delay(delayMilliseconds - (int)currentTaskRunnigMilliseconds);
                _taskService.RemoveTask(query.Id);
            }
            catch
            {
                throw;
            }
        });
        _taskService.AddTask(query.Id, insertQueryTask);
        return new() { QueryId = query.Id };
    }

    public async Task<QueryInfoResponse> GetQueryInfo(Guid queryId)
    {
        QueryInfoResponse response = new();
        if (_taskService.IsTaskNullOrCompleted(queryId))
        {
            var query = await _context.Queries.Include(x => x.User)
                 .ThenInclude(x => x.UserSessions).FirstOrDefaultAsync(x => x.Id == queryId);
            response = _mapper.Map<QueryInfoResponse>(query);
            response.Percent = (int)_taskService.GetTaskInvokationPercent(queryId, _configuration.GetValue<int>("DelayMillisecondsCount"));
            response.Result = new()
            {
                UserId = query.UserId,
                CountSignIn = query.User.UserSessions.Count(x => x.CreatedDate <= query.DateTo && x.CreatedDate >= query.DateFrom)
            };
            return response;
        }
        else
        {
            var query = await _context.Queries.FirstOrDefaultAsync(x => x.Id == queryId);
            response = _mapper.Map<QueryInfoResponse>(query);
            response.Percent = (int)_taskService.GetTaskInvokationPercent(queryId, _configuration.GetValue<int>("DelayMillisecondsCount"));
            return response;
        }
    }
}
