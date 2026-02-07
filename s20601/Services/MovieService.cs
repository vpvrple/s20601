using MediatR;
using Microsoft.EntityFrameworkCore;
using s20601.Data;
using s20601.Data.Models;
using s20601.Data.Models.DTOs;
using s20601.Events.Commands;
using s20601.Events.Queries;
using s20601.Services.External.Azure;

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
        //needs to be revisited
        using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.Movies
            .Include(x => x.MovieOfTheDay)
            .OrderBy(m => Guid.NewGuid())
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
        var movie = await context.Movies
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        var ovierview = await _mediator.Send(new GetTMDBMovieOverviewQuery(movie.IMDBId));
        movie.Overview = ovierview;
        return movie;
    }

    public async Task<string?> GetMoviePosterByMovieId(int id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        var movie = await context.Movies
            .Where(x => x.Id == id)
            .Select(x => new { x.Id, x.IMDBId, x.PosterPath })
            .FirstOrDefaultAsync();

        if (movie is null)
        {
            return null;
        }

        if (string.IsNullOrEmpty(movie.PosterPath))
        {
            return await _mediator.Send(new GetTMDBMovieImageQuery(movie.IMDBId));
        }
        return await _mediator.Send(new GetAzureMovieImageQuery(AzureBlobType.MovieImages, movie.PosterPath));
    }

    public async Task<string?> GetMoviePoster(string posterPath)
    {
        if (string.IsNullOrEmpty(posterPath))
        {
            return null;
        }
        
        return await _mediator.Send(new GetAzureMovieImageQuery(AzureBlobType.MovieImages, posterPath));
    }

    public async Task<string?> UploadMoviePoster(int movieId, Stream fileStream, string fileName)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        var movie = await context.Movies
            .Where(x => x.Id == movieId)
            .FirstOrDefaultAsync();

        var fileExtension = Path.GetExtension(fileName).ToLowerInvariant();
        var newFileName = $"{Guid.NewGuid()}{fileExtension}";

        await _mediator.Send(new UploadMoviePosterCommand(AzureBlobType.MovieImages, fileStream, newFileName));

        await context.SaveChangesAsync();

        return newFileName;
    }

    public async Task<string?> GetMovieOverviewById(int id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        var movie = await context.Movies
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        var overview = await _mediator.Send(new GetTMDBMovieOverviewQuery(movie.IMDBId));

        return overview;
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

        var reviews = await context.Reviews
            .Where(r => r.Movie_Id == id)
            .Select(r => r.Content)
            .ToListAsync();

        return new MovieWithRating
        {
            Id = movie.Id,
            Title = movie.Title,
            StartYear = movie.StartYear,
            Runtime = movie.RuntimeMinutes,
            MovieRatingSummary = ratingSummary ?? new MovieRatingSummary()
        };
    }

    public async Task<SentimentType> GetMovieSentimentByMovieId(int movieId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        var reviews = await context.Reviews
            .Where(r => r.Movie.Id == movieId)
            .Select(r => r.Content)
            .ToListAsync();
        var sentiment = await _mediator.Send(new GetAzureReviewsSentimentQuery(reviews));
        return sentiment;
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

    public async Task<List<MovieUpdateRequest>> GetMovieUpdateRequests(MovieUpdateRequestStatus? status = null)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        var query = context.MovieUpdateRequests.AsQueryable();

        if (status.HasValue)
        {
            query = query.Where(x => x.Status == status.Value);
        }

        return await query.ToListAsync();
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
                
                if (request.NewPosterPath != null)
                {
                    if (!string.IsNullOrEmpty(movie.PosterPath))
                    {
                        await _mediator.Send(new MoviePosterUpdatedCommand(AzureBlobType.MovieImages, movie.PosterPath));
                    }
                    movie.PosterPath = request.NewPosterPath;
                }
                
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
                            var newCrew = new Crew
                            {
                                FirstName = crewRequest.FirstName,
                                LastName = crewRequest.LastName,
                                BirthYear = crewRequest.BirthYear ?? 0,
                                DeathYear = crewRequest.DeathYear
                            };
                            
                            context.MovieCrews.Add(new MovieCrew
                            {
                                Movie_Id = movie.Id,
                                IdCrewNavigation = newCrew,
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
            var newMovie = new Movie
            {
                Title = request.NewTitle ?? "Untitled",
                OriginalTitle = request.NewOriginalTitle ?? "Untitled",
                StartYear = request.NewStartYear ?? DateTime.UtcNow.Year,
                EndYear = request.NewEndYear,
                RuntimeMinutes = request.NewRuntimeMinutes ?? 0,
                TitleType = request.NewTitleType ?? "Movie",
                PosterPath = request.NewPosterPath
            };

            context.Movies.Add(newMovie);

            foreach (var genre in request.NewGenres)
            {
                context.MovieGenres.Add(new MovieGenre
                {
                    Movie = newMovie,
                    Genre_Id = genre.Id
                });
            }
            
            foreach (var crewRequest in request.NewCrew)
            {
                if (crewRequest.CrewId.HasValue)
                {
                    context.MovieCrews.Add(new MovieCrew
                    {
                        Movie = newMovie,
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

                    context.MovieCrews.Add(new MovieCrew
                    {
                        Movie = newMovie,
                        IdCrewNavigation = newCrew,
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

    public async Task<GetPersonaWithDetails?> GetPersonaWithDetails(int id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        
        var crew = await context.Crews
            .Where(c => c.Id == id)
            .Include(c => c.MovieCrews)
            .ThenInclude(mc => mc.Movie)
            .FirstOrDefaultAsync();

        if (crew == null)
        {
            return null;
        }

        return new GetPersonaWithDetails
        {
            Id = crew.Id,
            IMDBId = crew.IMDBId,
            FirstName = crew.FirstName,
            LastName = crew.LastName,
            BirthYear = crew.BirthYear,
            DeathYear = crew.DeathYear,
            Movies = crew.MovieCrews.Select(mc => new PersonaMovieRole
            {
                MovieId = mc.Movie.Id,
                MovieUrl = mc.Movie.GetUrl(),
                Title = mc.Movie.Title,
                StartYear = mc.Movie.StartYear,
                Job = mc.Job,
                CharacterName = mc.CharacterName
            }).ToList()
        };
    }
    
    
    public async Task<string?> GetPersonaImageById(int id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        var persona = await context.Crews
            .Where(x => x.Id == id)
            .Select(x => x.IMDBId)
            .FirstOrDefaultAsync();

        if (persona is null)
        {
            return null;
        }
        
        return await _mediator.Send(new GetTMDBPersonaImageQuery(persona));
    }
    
    public async Task<string?> GetPersonaBiographyById(int id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        var person = await context.Crews
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    
        var image = await _mediator.Send(new GetTMDBPersonaBiographyQuery(person.IMDBId));
    
        return image;
    }
}