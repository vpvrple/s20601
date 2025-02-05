using Microsoft.EntityFrameworkCore;
using s20601.Data;
using s20601.Data.Models;
namespace s20601.Services;

public class MovieService : IMovieService
{
    private readonly IDbContextFactory<S20601Context> _dbContextFactory;
    public MovieService(IDbContextFactory<S20601Context> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<MovieOfTheDay?> GetCurrentMovieOfTheDayAsync()
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.MovieOfTheDays
            .Include(x => x.Movie)
            .OrderByDescending(x => x.Date)
            .FirstAsync();
    }

}
