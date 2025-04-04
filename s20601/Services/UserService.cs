using Microsoft.EntityFrameworkCore;
using s20601.Data;
using s20601.Data.Models.DTOs;

namespace s20601.Services;

public class UserService : IUserService
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    public UserService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<ApplicationUser?> GetUserByUsernameAsync(string username)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.Users
            .FirstOrDefaultAsync(x => x.UserName == username);
    }

    public async Task<List<ApplicationUser>> GetTopUsersByPoints()
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.Users
               .OrderByDescending(x => x.ReputationPoints)
               .ToListAsync();
    }

}
