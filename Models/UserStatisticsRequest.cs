namespace AsyncProcessing.Models;
public class UserStatisticsRequest
{
    public Guid UserId { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
}
