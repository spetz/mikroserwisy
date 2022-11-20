using Micro.Handlers;
using Microsoft.EntityFrameworkCore;
using VideoHub.Services.Users.Core.DAL;
using VideoHub.Services.Users.Core.DTO;

namespace VideoHub.Services.Users.Core.Queries.Handlers;

internal sealed class GetUserHandler : IQueryHandler<GetUser, UserDto?>
{
    private readonly UsersDbContext _dbContext;

    public GetUserHandler(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserDto?> HandleAsync(GetUser query, CancellationToken cancellationToken = default)
    {
        var user = await _dbContext.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == query.UserId, cancellationToken);

        return user is null ? default : new UserDto(user.Id, user.Username);
    }
}