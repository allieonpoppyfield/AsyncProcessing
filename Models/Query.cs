namespace AsyncProcessing.Models;

public class Query
{
    [Key]
    public Guid Id { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public long CreatedDateUnixTime { get; set; }
}
