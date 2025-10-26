using Microsoft.EntityFrameworkCore;
using s20601.Data;
using s20601.Data.Models;

namespace s20601.Services;

public class MovieCollectionService : IMovieCollectionService
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    public MovieCollectionService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<List<MovieCollection>> GetUserMovieCollections(string userId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var collections = await context.MovieCollections
            .Include(x => x.MovieCollectionUsers)
            .Where(x => x.MovieCollectionUsers.Any(y => y.IdUser == userId))
            .ToListAsync();

        return collections ?? [];
    }
}
