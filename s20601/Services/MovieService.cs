using System.Linq.Expressions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using s20601.Data.Models;
using s20601.Data.Models.DTOs;
using s20601.Data;
using s20601.Events.Commands;

namespace s20601.Services;

public class MovieService : IMovieService
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IMediator _mediator;
    public MovieService(IDbContextFactory<ApplicationDbContext> dbContextFactory, IMediator mediator)
    {
        _dbContextFactory = dbContextFactory;
        _mediator = mediator;
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

    public async Task<List<Movie>> GetTrendingMovies(int n)
    {
        //needs to be revisited
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var movies = await context.Movies
            .Take(n)
            .ToListAsync();

        return movies ?? [];
    }

    public async Task<Movie?> GetMovieByIdAsync(int id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.Movies
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }
    
    public async Task<MovieWithRating?> GetMovieWithRatingById(int id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        var movie = await context.Movies
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        if (movie is null)
        {
            return null;
        }

        var ratingSummary = await context.MovieRates
            .Where(r => r.Movie_Id == id)
            .GroupBy(r => r.Movie_Id)
            .Select(g => new MovieRatingSummary
            {
                AvgRating = g.Average(r => r.Rating),
                RateCount = g.Count()
            })
            .FirstOrDefaultAsync();

        return new MovieWithRating
        {
            Id = movie.Id,
            Title = movie.Title,
            StartYear = movie.StartYear,
            Runtime = movie.RuntimeMinutes,
            MovieRatingSummary = ratingSummary ?? new MovieRatingSummary()
        };
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

    public async Task<List<Genre>> GetAllGenresAsync()
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.Genres.ToListAsync();
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
                BirthYear = x.IdCrewNavigation.BirthYear,
                DeathYear = x.IdCrewNavigation.DeathYear,
                Job = x.Job,
                CharacterName = x.CharacterName,
            })
            .ToListAsync();

        return crew ?? [];
    }

    public async Task AddMovieUpdateRequest(MovieUpdateRequest request)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        
        request.CreatedAt = DateTime.UtcNow;
        request.Status = MovieUpdateRequestStatus.Open;

        if (request.NewGenres != null && request.NewGenres.Any())
        {
            var genreIds = request.NewGenres.Select(g => g.Id).ToList();
            var existingGenres = await context.Genres.Where(g => genreIds.Contains(g.Id)).ToListAsync();
            request.NewGenres = existingGenres;
        }
        
        context.MovieUpdateRequests.Add(request);
        await context.SaveChangesAsync();
    }

    public async Task<MovieUpdateRequest?> GetMovieUpdateRequest(int id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        return await context.MovieUpdateRequests
            .Include(x => x.Movie)
            .Include(x => x.NewGenres)
            .Include(x => x.NewCrew)
            .Include(x => x.IdUserNavigation)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<MovieUpdateRequest>> GetMovieUpdateRequests(Expression<Func<MovieUpdateRequest, bool>>? predicate = null)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        var query = context.MovieUpdateRequests.AsQueryable();

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return await query.ToListAsync();
    }

    public async Task UpdateMovie(Movie movie)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        
        var movieToUpdate = context.Movies.Where(x => x. Id == movie.Id);
        
        
        context.Movies.Update(movie);
        await context.SaveChangesAsync();
    }

    public async Task<List<GetMovieCrewMemberWithDetails>> SearchCrewAsync(string query, int? movieId = null)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        
        IQueryable<Crew> queryable = context.Crews;

        if (!string.IsNullOrWhiteSpace(query))
        {
            queryable = queryable.Where(c => c.FirstName.Contains(query) || c.LastName.Contains(query));
        }

        var crewList = await queryable.Take(10).ToListAsync();
        var result = new List<GetMovieCrewMemberWithDetails>();

        foreach (var c in crewList)
        {
            var dto = new GetMovieCrewMemberWithDetails
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                BirthYear = c.BirthYear,
                DeathYear = c.DeathYear,
                Job = "",
                CharacterName = ""
            };

            if (movieId.HasValue)
            {
                var movieCrew = await context.MovieCrews
                    .FirstOrDefaultAsync(mc => mc.IdCrew == c.Id && mc.Movie_Id == movieId.Value);
                
                if (movieCrew != null)
                {
                    dto.Job = movieCrew.Job;
                    dto.CharacterName = movieCrew.CharacterName;
                }
            }

            result.Add(dto);
        }

        return result;
    }

    public async Task ApproveMovieUpdateRequest(int requestId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        var request = await context.MovieUpdateRequests
            .Include(x => x.Movie)
            .Include(x => x.NewGenres)
            .Include(x => x.NewCrew)
            .FirstOrDefaultAsync(x => x.Id == requestId);

        if (request is not { Status: MovieUpdateRequestStatus.Open })
        {
            return;
        }

        if (request.Movie_Id.HasValue)
        {
            // Update existing movie
            var movie = await context.Movies
                .Include(m => m.MovieGenres)
                .Include(m => m.MovieCrews)
                .FirstOrDefaultAsync(m => m.Id == request.Movie_Id);

            if (movie != null)
            {
                if (request.NewTitle != null) movie.Title = request.NewTitle;
                if (request.NewOriginalTitle != null) movie.OriginalTitle = request.NewOriginalTitle;
                if (request.NewStartYear.HasValue) movie.StartYear = request.NewStartYear.Value;
                if (request.NewEndYear.HasValue) movie.EndYear = request.NewEndYear.Value;
                if (request.NewRuntimeMinutes.HasValue) movie.RuntimeMinutes = request.NewRuntimeMinutes.Value;
                if (request.NewTitleType != null) movie.TitleType = request.NewTitleType;

                // Update Genres
                if (request.NewGenres.Count != 0)
                {
                    context.MovieGenres.RemoveRange(movie.MovieGenres);
                    foreach (var genre in request.NewGenres)
                    {
                        context.MovieGenres.Add(new MovieGenre
                        {
                            Movie_Id = movie.Id,
                            Genre_Id = genre.Id
                        });
                    }
                }

                // Update Crew
                if (request.NewCrew.Count != 0)
                {
                    context.MovieCrews.RemoveRange(movie.MovieCrews);
                    foreach (var crewRequest in request.NewCrew)
                    {
                        if (crewRequest.CrewId.HasValue)
                        {
                            context.MovieCrews.Add(new MovieCrew
                            {
                                Movie_Id = movie.Id,
                                IdCrew = crewRequest.CrewId.Value,
                                Job = crewRequest.Job,
                                CharacterName = crewRequest.CharacterName
                            });
                        }
                        else
                        {
                            // Create new crew member if needed, though typically we expect CrewId to be populated for existing crew
                            // If CrewId is null, it implies a new person needs to be created first.
                            // Assuming for now we only link existing crew or create new crew if names provided
                            var newCrew = new Crew
                            {
                                FirstName = crewRequest.FirstName,
                                LastName = crewRequest.LastName,
                                BirthYear = crewRequest.BirthYear ?? 0,
                                DeathYear = crewRequest.DeathYear
                            };
                            context.Crews.Add(newCrew);
                            await context.SaveChangesAsync(); // Save to get ID

                            context.MovieCrews.Add(new MovieCrew
                            {
                                Movie_Id = movie.Id,
                                IdCrew = newCrew.Id,
                                Job = crewRequest.Job,
                                CharacterName = crewRequest.CharacterName
                            });
                        }
                    }
                }
            }
        }
        else
        {
            // Create new movie
            var newMovie = new Movie
            {
                Title = request.NewTitle ?? "Untitled",
                OriginalTitle = request.NewOriginalTitle ?? "Untitled",
                StartYear = request.NewStartYear ?? DateTime.UtcNow.Year,
                EndYear = request.NewEndYear,
                RuntimeMinutes = request.NewRuntimeMinutes ?? 0,
                TitleType = request.NewTitleType ?? "Movie"
            };

            context.Movies.Add(newMovie);
            await context.SaveChangesAsync(); // Save to get ID

            // Add Genres
            foreach (var genre in request.NewGenres)
            {
                context.MovieGenres.Add(new MovieGenre
                {
                    Movie_Id = newMovie.Id,
                    Genre_Id = genre.Id
                });
            }

            // Add Crew
            foreach (var crewRequest in request.NewCrew)
            {
                 if (crewRequest.CrewId.HasValue)
                {
                    context.MovieCrews.Add(new MovieCrew
                    {
                        Movie_Id = newMovie.Id,
                        IdCrew = crewRequest.CrewId.Value,
                        Job = crewRequest.Job,
                        CharacterName = crewRequest.CharacterName
                    });
                }
                else
                {
                    var newCrew = new Crew
                    {
                        FirstName = crewRequest.FirstName,
                        LastName = crewRequest.LastName,
                        BirthYear = crewRequest.BirthYear ?? 0,
                        DeathYear = crewRequest.DeathYear
                    };
                    context.Crews.Add(newCrew);
                    await context.SaveChangesAsync();

                    context.MovieCrews.Add(new MovieCrew
                    {
                        Movie_Id = newMovie.Id,
                        IdCrew = newCrew.Id,
                        Job = crewRequest.Job,
                        CharacterName = crewRequest.CharacterName
                    });
                }
            }
        }

        request.Status = MovieUpdateRequestStatus.Approved;
        await _mediator.Publish(new MovieRequestApprovedCommand(request.IdUser, 10));
        await context.SaveChangesAsync();
    }

    public async Task RejectMovieUpdateRequest(int requestId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        var request = await context.MovieUpdateRequests.FindAsync(requestId);

        if (request != null && request.Status == MovieUpdateRequestStatus.Open)
        {
            request.Status = MovieUpdateRequestStatus.Rejected;
            await context.SaveChangesAsync();
        }
    }
}
