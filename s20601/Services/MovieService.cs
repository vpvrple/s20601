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
            .OrderByDescending(x => x.Date)
            .FirstOrDefaultAsync();
    }

    public async Task<Movie?> GetMovieDataByIdAsync(string id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.Movies
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<int> GetMovieRatingByIdAsync(string id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var ratingSum = await context.MovieRates
            .Where(x => x.Movie_Id == id)
            .SumAsync(x => x.Rating);

        var rateCount = await context.MovieRates
            .Where(x => x.Movie_Id == id)
            .CountAsync();

        if (rateCount == 0)
            return 0;

        return (int)Math.Round((double)(ratingSum / rateCount));
    }
}
