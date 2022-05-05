namespace AsyncProcessing.Models;

public class QueryInfoResponse
{
    public Guid QueryId { get; set; }
    public int Percent { get; set; }
    public QueryInfoResult? Result { get; set; }
}

public class QueryInfoResult
{
    public Guid UserId { get; set; }
    public int CountSignIn { get; set; }
}
