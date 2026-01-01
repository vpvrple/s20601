using Microsoft.EntityFrameworkCore;
using s20601.Data;
using s20601.Data.Models;

namespace s20601.Services;

public class RankingService : IRankingService
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    public RankingService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }
    public async Task<List<ApplicationUser>> GetTopUsersByPoints()
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.Users
               .OrderByDescending(x => x.ReputationPoints)
               .ToListAsync();
    }
    
    public async Task IncrementPoints(string userId, int points)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        
        await context.Users
            .Where(u => u.Id == userId)
            .ExecuteUpdateAsync(setters => setters.SetProperty(u => u.ReputationPoints, u => u.ReputationPoints + points));
    }
    
    public async Task DecrementPoints(string userId, int points)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        
        await context.Users
            .Where(u => u.Id == userId)
            .ExecuteUpdateAsync(setters => setters.SetProperty(u => u.ReputationPoints, u => u.ReputationPoints - points));
    }

    public async Task<int> GetUserPointsById(string userId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        return await context.Users
            .Where(u => u.Id == userId)
            .Select(x => x.ReputationPoints)
            .FirstOrDefaultAsync();
    }
}
