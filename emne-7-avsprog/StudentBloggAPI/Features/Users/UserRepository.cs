using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StudentBloggAPI.Data;
using StudentBloggAPI.Features.Users.Interfaces;

namespace StudentBloggAPI.Features.Users;

public class UserRepository : IUserRepository
{
    private readonly ILogger<UserRepository> _logger;
    private readonly StudentBloggDbContext _dbContext;

    public UserRepository(
        ILogger<UserRepository> logger,
        StudentBloggDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    
    public async Task<User?> AddAsync(User entity)
    {
        await _dbContext.Users.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public Task<User?> UpdateAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> DeleteByIdAsync(Guid id)
    {
        var user = await _dbContext.Users.FindAsync(id);
        await _dbContext.Users
            .Where(u => u.Id == id)
            .ExecuteDeleteAsync();
        
        await _dbContext.SaveChangesAsync();
        return user;

    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<IEnumerable<User>> GetPagedAsync(int pageNumber, int pageSize)
    {
        int skip = (pageNumber - 1) * pageSize;

        var users = await _dbContext.Users
            .OrderBy(u => u.Id)
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();

        return users;
    }

    public async Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate)
    {
        return await _dbContext.Users
            .Where(predicate)
            .ToListAsync();
    }
}