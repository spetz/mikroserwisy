namespace VideoHub.Services.Videos.Core.Entities;

public class Video
{
    public long Id { get; private set; }
    public long UserId { get; private set; }
    public string Title { get; private set; } = null!;
    public long Size { get; private set; }
    public int Length { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public VideoState State { get; private set; }

    private Video()
    {
    }

    public Video(long id, long userId, string title, long size, TimeSpan length, DateTime createdAt)
    {
        Id = id;
        UserId = userId;
        Title = title;
        Size = size;
        Length = (int) length.TotalMinutes;
        CreatedAt = createdAt;
        State = VideoState.Received;
    }

    public void SetState(VideoState state) => State = state;
}