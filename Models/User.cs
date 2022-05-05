namespace AsyncProcessing.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }
    public List<UserSession> UserSessions { get; set; }
    public List<Query> Queries { get; set; }
}
