namespace VideoHub.Services.Channels.Core.Entities;

public class User
{
    public long Id { get; private set; }
    public string Username { get; private set; } = null!;

    private User()
    {
    }

    public User(long id, string username)
    {
        Id = id;
        Username = username;
    }
}