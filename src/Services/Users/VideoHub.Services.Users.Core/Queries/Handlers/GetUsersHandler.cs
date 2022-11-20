using Micro.Handlers;
using Microsoft.EntityFrameworkCore;
using VideoHub.Services.Users.Core.DAL;
using VideoHub.Services.Users.Core.DTO;

namespace VideoHub.Services.Users.Core.Queries.Handlers;

internal sealed class GetUsersHandler : IQueryHandler<GetUsers, IEnumerable<UserDto>>
{
    private readonly UsersDbContext _dbContext;

    public GetUsersHandler(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<UserDto>> HandleAsync(GetUsers query, CancellationToken cancellationToken = default)
        => await _dbContext.Users
            .AsNoTracking()
            .Where(x => ((query.Username != null && x.Username.Contains(query.Username)) || query.Username == null))
            .Select(x => new UserDto(x.Id, x.Username))
            .ToListAsync(cancellationToken);
}