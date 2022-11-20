namespace VideoHub.Services.Channels.Core.Entities;

public class Channel
{
    public long Id { get; private set; }
    public long UserId { get; private set; }
    public User User { get; private set; } = null!;
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;

    private Channel()
    {
    }

    public Channel(long id, long userId, string name, string? description = default)
    {
        Id = id;
        UserId = userId;
        Name = name;
        Description = description ?? string.Empty;
    }
}