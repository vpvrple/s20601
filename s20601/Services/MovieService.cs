using Microsoft.EntityFrameworkCore;
using s20601.Data;
using s20601.Data.Models;
using s20601.Data.Models.DTOs;
namespace s20601.Services;

public class MovieService : IMovieService
{
    private readonly IDbContextFactory<S20601Context> _dbContextFactory;
    public MovieService(IDbContextFactory<S20601Context> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<Movie> GetMovieOfTheDayAsync()
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.Movies
            .Include(x => x.MovieOfTheDay)
            .OrderByDescending(x => x.MovieOfTheDay.Date)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Movie>> GetPastMoviesOfTheDay(int n)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var movies = await context.Movies
            .Include(x => x.MovieOfTheDay)
            .Where(x => x.Id == x.MovieOfTheDay.Movie_Id)
            .OrderByDescending(x => x.MovieOfTheDay.Date)
            .Skip(1)
            .Take(n)
            .ToListAsync();

        return movies ?? new List<Movie>();
    }

    public async Task<List<MovieCollection>> GetTrendingMovieCollections(int n)
    {
        // needs to be revisited
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var movieCollections = await context.MovieCollections
            .Take(n)
            .ToListAsync();

        return movieCollections ?? new List<MovieCollection>();
    }

    public async Task<Movie?> GetMovieByIdAsync(string id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.Movies
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<double> GetMovieRatingByIdAsync(string id)
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

        return Math.Round((double)(ratingSum / rateCount));
    }

    public async Task<List<Genre>> GetMovieGenresByIdAsync(string id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var genres = await context.MovieGenres
            .Where(x => x.Movie_Id == id)
            .Include(x => x.Genre)
            .Select(x => x.Genre)
            .ToListAsync();

        return genres ?? new List<Genre>();
    }

    public async Task<List<GetMovieCrewMemberWithDetails>> GetMovieCrewByMovieIdAsync(string id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var crew = await context.MovieCrews
            .Where(x => x.Movie_Id == id)
            .Include(x => x.IdCrewNavigation)
            .Select(x => new GetMovieCrewMemberWithDetails
            {
                Id = x.IdCrewNavigation.Id,
                FirstName = x.IdCrewNavigation.FirstName,
                LastName = x.IdCrewNavigation.LastName,
                Job = x.Job,
                CharacterName = x.CharacterName,
            })
            .ToListAsync();

        return crew ?? new List<GetMovieCrewMemberWithDetails>();
    }


    public async Task<List<GetMovieReviewWithRating>> GetMovieReviewsByMovieIdAsync(string id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var reviews = await context.Reviews
            .Where(x => x.Movie_Id == id)
            .Include(x => x.ReviewRates)
            .ThenInclude(x => x.IdUserNavigation)
            .Select(x => new GetMovieReviewWithRating
            {
                UserName = x.IdAuthorNavigation.UserName,
                CreatedAt = x.CreatedAt,
                Content = x.Content,
                LikeRating = x.ReviewRates.Select(x => x.Rating).Count(x => x == 1),
                DislikeRating = x.ReviewRates.Select(x => x.Rating).Count(x => x == 0)
            })
            .ToListAsync();

        return reviews ?? new List<GetMovieReviewWithRating>();
    }

    public async Task<List<GetMovieReviewWithRating>> GetMovieReviewsByMovieIdAsync(string id, int n)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var reviews = await context.Reviews
            .Where(x => x.Movie_Id == id)
            .Include(x => x.ReviewRates)
            .ThenInclude(x => x.IdUserNavigation)
            .Select(x => new GetMovieReviewWithRating
            {
                UserName = x.IdAuthorNavigation.UserName,
                CreatedAt = x.CreatedAt,
                Content = x.Content,
                LikeRating = x.ReviewRates.Select(x => x.Rating).Count(x => x == 1),
                DislikeRating = x.ReviewRates.Select(x => x.Rating).Count(x => x == 0)
            })
            .Take(n)
            .ToListAsync();

        return reviews ?? new List<GetMovieReviewWithRating>();
    }

    public async Task<List<GetTopMovieByRating>> GetTopMoviesByRatingAsync(int n)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var top100 = await context.Movies
            .Join(context.MovieRates,
            x => x.Id,
            y => y.Movie_Id,
            (x, y) => new { 
                x,y
            })
            .GroupBy(joined => joined.x.Id)
            .Select(movieWithRating => new GetTopMovieByRating
            {
                Id = movieWithRating.Key,
                Title = movieWithRating.Select(x => x.x.Title).First(),
                Rating = movieWithRating.Average(x => x.y.Rating),
                RateCount = movieWithRating.Count(),
                Runtime = movieWithRating.Select(x => x.x.RuntimeMinutes).First(),
                StartYear = movieWithRating.Select(x => x.x.StartYear).First()
            })
            .OrderByDescending(x => x.Rating)
            .Take(n)
            .ToListAsync();
            

        return top100;
    }
}
