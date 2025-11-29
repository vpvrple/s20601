using Microsoft.EntityFrameworkCore;
using s20601.Data;
using s20601.Data.Models;

namespace s20601.Services;

public class SearchService : ISearchService
{

    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    public SearchService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }
    public async Task<List<Movie>> SearchMovieByTitle(string title)
    {
        //needs to be revisited
        using var context = await _dbContextFactory.CreateDbContextAsync();

        // FOR NOW JUST SIMPLE CONTAINS, BETTER IMPLEMENTATION NEEDED
        var movieTitles = await context.Movies
            .Where(x => x.Title.Contains(title) || x.OriginalTitle.Contains(title))
            .ToListAsync();

        return movieTitles ?? [];
    }
}
