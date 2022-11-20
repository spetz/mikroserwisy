using Microsoft.EntityFrameworkCore;
using VideoHub.Services.Users.Core.Entities;
using VideoHub.Services.Users.Core.Repositories;

namespace VideoHub.Services.Users.Core.DAL.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly UsersDbContext _context;
    private readonly DbSet<User> _users;

    public UserRepository(UsersDbContext context)
    {
        _context = context;
        _users = _context.Users;
    }

    public Task<User?> GetAsync(long id)
        => _users.Include(x => x.Role).SingleOrDefaultAsync(x => x.Id == id);

    public  Task<User?> GetByEmailAsync(string email)
        => _users.Include(x => x.Role).SingleOrDefaultAsync(x => x.Email == email);

    public Task<User?> GetByUsernameAsync(string username)
        => _users.Include(x => x.Role).SingleOrDefaultAsync(x => x.Username == username);

    public async Task AddAsync(User user)
    {
        await _users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}