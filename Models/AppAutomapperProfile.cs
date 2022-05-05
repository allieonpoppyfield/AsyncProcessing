namespace AsyncProcessing.Models;

public class AppAutomapperProfile : Profile
{
    public AppAutomapperProfile()
    {
        CreateMap<UserStatisticsRequest, Query>();
        CreateMap<Query, QueryInfoResponse>().ForMember(res => res.QueryId, q => q.MapFrom(x => x.Id));
    }
}
