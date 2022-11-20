namespace VideoHub.Services.Users.Core.Entities;

public class UserSubscription
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public User User { get; set; } = null!;
    public long SizeLimit { get; set; }
    public long VideosLimit { get; set; }
    public long LengthLimit { get; set; }
    public int Version { get; set; }
}