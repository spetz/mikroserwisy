namespace VideoHub.Services.Channels.Core.Entities;

public class Video
{
    public long Id { get; private set; }
    public long UserId { get; private set; }
    public User User { get; private set; } = null!;
    public string Title { get; private set; } = null!;

    private Video()
    {
    }

    public Video(long id, long userId, string title)
    {
        Id = id;
        UserId = userId;
        Title = title;
    }
}