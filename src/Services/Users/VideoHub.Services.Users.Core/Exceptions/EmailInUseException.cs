using Micro.Exceptions;

namespace VideoHub.Services.Users.Core.Exceptions;

internal class EmailInUseException : CustomException
{
    public string Email { get; }

    public EmailInUseException(string email) : base($"Email: '{email}' is already in use.")
    {
        Email = email;
    }
}