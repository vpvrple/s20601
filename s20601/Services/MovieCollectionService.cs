using Microsoft.EntityFrameworkCore;
using s20601.Data;
using s20601.Data.Models;
using s20601.Data.Models.DTOs;

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
            .Where(mc => mc.MovieCollectionUsers.Any(mu => mu.IdUser == userId))
            .Include(mc => mc.MovieCollectionUsers)
            .ToListAsync();

        return collections ?? [];
    }

    public async Task<MovieCollection> CreateMovieCollection(string name, string userId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        var newMovieCollection = new MovieCollection
        {
            Name = name,
            Description = "",
            CreatedAt = DateTime.UtcNow,
            MovieCollectionUsers =
            [
                new() { IdUser = userId }
            ],
            MovieCollectionMovie = null
        };
        context.MovieCollections.Add(newMovieCollection);
        await context.SaveChangesAsync();
        return newMovieCollection;
    }

    public async Task DeleteMovieCollection(int collectionId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        var collection = await context.MovieCollections
            .FirstOrDefaultAsync(x => x.Id == collectionId);
        if (collection != null)
        {
            context.MovieCollections.Remove(collection);
            await context.SaveChangesAsync();
        }
    }

    public async Task<MovieCollection?> GetMovieCollectionById(int collectionId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.MovieCollections
            .Include(x => x.MovieCollectionUsers)
            .FirstOrDefaultAsync(x => x.Id == collectionId);
    }

    public async Task<List<MovieWithRating>> GetMoviesWithRatingOfMovieCollectionById(int collectionId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        var movies = await context.MovieCollectionMovies
            .Where(mcm => mcm.IdMovieCollection == collectionId)
            .Include(mcm => mcm.Movie)
            .ThenInclude(m => m.MovieRates)
            .Select(mcm => new MovieWithRating
            {
                Id = mcm.Movie.Id,
                Title = mcm.Movie.Title,
                StartYear = mcm.Movie.StartYear,
                Runtime = mcm.Movie.RuntimeMinutes,
                MovieRatingSummary = mcm.Movie.MovieRates.Any() ? mcm.Movie.MovieRates.GroupBy(mr => mr.Movie_Id).Select(g => new MovieRatingSummary
                {
                    AvgRating = g.Average(mr => mr.Rating),
                    RateCount = g.Count()
                }).FirstOrDefault() : new MovieRatingSummary { AvgRating = 0, RateCount = 0 }
            })
            .ToListAsync();
        return movies;
    }

    public async Task UpdateMovieCollection(MovieCollection collection)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        context.MovieCollections.Update(collection);
        await context.SaveChangesAsync();
    }

    public async Task AddMovieToMovieCollection(int collectionId, int movieId, int userId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var collectionMovie = new MovieCollectionMovie
        {
            IdMovieCollection = collectionId,
            Movie_Id = movieId,
            AddedAt = DateTime.UtcNow
        };

        context.MovieCollectionUsers.Add(new MovieCollectionUser
        {
            IdMovieCollection = collectionId,
            IdUser = userId.ToString()
        });

        context.MovieCollectionMovies.Add(collectionMovie);
        await context.SaveChangesAsync();
    }
}
