using Microsoft.EntityFrameworkCore;
using s20601.Data.Models;
using s20601.Data.Models.DTOs;
using s20601.Data;

namespace s20601.Services;

public class MovieService : IMovieService
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    public MovieService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<Movie?> GetMovieOfTheDayAsync()
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

        return movies ?? [];
    }

    public async Task<List<MovieCollection>> GetTrendingMovieCollections(int n)
    {
        //needs to be revisited
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var movieCollections = await context.MovieCollections
            .Take(n)
            .ToListAsync();

        return movieCollections ?? [];
    }

    public async Task<Movie?> GetMovieByIdAsync(int id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.Movies
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Genre>> GetMovieGenresByIdAsync(int id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var genres = await context.MovieGenres
            .Where(x => x.Movie_Id == id)
            .Include(x => x.Genre)
            .Select(x => x.Genre)
            .ToListAsync();

        return genres ?? [];
    }

    public async Task<List<GetMovieCrewMemberWithDetails>> GetMovieCrewByMovieIdAsync(int id)
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

        return crew ?? [];
    }
}
