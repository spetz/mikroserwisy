using Micro.Handlers;
using Microsoft.EntityFrameworkCore;
using VideoHub.Services.Users.Core.DAL;
using VideoHub.Services.Users.Core.DTO;

namespace VideoHub.Services.Users.Core.Queries.Handlers;

internal sealed class GetAccountHandler : IQueryHandler<GetAccount, AccountDto?>
{
    private readonly UsersDbContext _dbContext;

    public GetAccountHandler(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AccountDto?> HandleAsync(GetAccount query, CancellationToken cancellationToken = default)
    {
        var user = await _dbContext.Users
            .AsNoTracking()
            .Include(x => x.Role)
            .SingleOrDefaultAsync(x => x.Id == query.UserId, cancellationToken);

        return user is null
            ? default
            : new AccountDto(user.Id, user.Email, user.Username, user.Role.Name, user.CreatedAt);
    }
}