namespace AsyncProcessing.Models;

public class UserSession
{
    [Key]
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public DateTime CreatedDate { get; set; }
}
