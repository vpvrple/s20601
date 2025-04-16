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
}
