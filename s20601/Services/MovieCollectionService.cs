using Microsoft.EntityFrameworkCore;
using s20601.Data;
using s20601.Data.Models;
using s20601.Data.Models.DTOs;

namespace s20601.Services;

public class MovieCollectionService : IMovieCollectionService
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly ICurrentUserService _currentUserService;
    public MovieCollectionService(IDbContextFactory<ApplicationDbContext> dbContextFactory, ICurrentUserService currentUserService)
    {
        _dbContextFactory = dbContextFactory;
        _currentUserService = currentUserService;
    }

    public async Task<List<MovieCollection>> GetUserMovieCollections()
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var authenticatedUserId = await _currentUserService.GetAuthenticatedUserId();

        var collections = await context.MovieCollections
            .Where(mc => mc.MovieCollectionUsers.Any(mu => mu.IdUser == authenticatedUserId))
            .Include(mc => mc.MovieCollectionUsers)
            .ToListAsync();

        return collections;
    }

    public async Task<List<GetMovieCollectionWithDetails>> GetMovieCollectionsWithDetails(string userId, MovieCollectionFilter filter = MovieCollectionFilter.All)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var query = context.MovieCollections
            .Include(mc => mc.MovieCollectionUsers)
            .ThenInclude(mcu => mcu.IdUserNavigation)
            .AsQueryable();

        if (filter == MovieCollectionFilter.PublicOnly)
        {
            query = query.Where(mc => mc.MovieCollectionUsers.Any(mcu => mcu.IdUser == userId) && mc.Visibility == CollectionVisibility.Public);
        }
        else
        {
            query = query.Where(mc => mc.MovieCollectionUsers.Any(mcu => mcu.IdUser == userId));
        }

        var collectionsFromDb = await query.ToListAsync();

        var collections = collectionsFromDb.Select(mc => new GetMovieCollectionWithDetails
        {
            Id = mc.Id,
            Name = mc.Name,
            Description = mc.Description,
            CreatedAt = mc.CreatedAt,
            Type = mc.Type,
            Visibility = mc.Visibility,
            Members = mc.MovieCollectionUsers.ToDictionary(mcu => mcu.IdUserNavigation, mcu => mcu.Role)
        })
            .ToList();

        return collections;
    }
    public async Task<List<MovieCollection>> GetNRecentUserMovieColletions(string userId, int n)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var collections = await context.MovieCollections
            .Where(mc => mc.MovieCollectionUsers.Any(mu => mu.IdUser == userId))
            .Include(mc => mc.MovieCollectionUsers)
            .OrderByDescending(x => x.CreatedAt)
            .Take(n)
            .ToListAsync();

        return collections;
    }


    public async Task<GetMovieCollectionWithDetails> CreateMovieCollection(string name, string? description, string userId, CollectionVisibility visibility)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        var newMovieCollection = new MovieCollection
        {
            Name = name,
            Description = description!,
            CreatedAt = DateTime.UtcNow,
            MovieCollectionUsers =
            [
                new() { IdUser = userId, Role = CollectionRole.Owner }
            ],
            MovieCollectionMovie = null,
            Visibility = visibility
        };
        context.MovieCollections.Add(newMovieCollection);
        await context.SaveChangesAsync();

        var user = await context.Users.FindAsync(userId);

        return new GetMovieCollectionWithDetails
        {
            Id = newMovieCollection.Id,
            Name = newMovieCollection.Name,
            Description = newMovieCollection.Description,
            CreatedAt = newMovieCollection.CreatedAt,
            Type = newMovieCollection.Type,
            Visibility = newMovieCollection.Visibility,
            Members = new Dictionary<ApplicationUser, CollectionRole>
            {
                { user, CollectionRole.Owner }
            }
        };
    }

    public async Task DeleteMovieCollection(int collectionId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        var collection = await context.MovieCollections
            .FirstOrDefaultAsync(x => x.Id == collectionId);

        if (collection.Type != CollectionType.Custom)
        {
            throw new InvalidOperationException("You cannot delete system collections.");
        }

        if (collection != null)
        {
            context.MovieCollections.Remove(collection);
            await context.SaveChangesAsync();
        }
    }

    public async Task DeleteMoviesFromMovieCollection(IEnumerable<int> movieIds, int collectionId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var movieLinks = await context.MovieCollectionMovies
            .Where(x => x.IdMovieCollection == collectionId && movieIds.Contains(x.Movie_Id))
            .ToListAsync();

        if (movieLinks.Any())
        {
            context.MovieCollectionMovies.RemoveRange(movieLinks);
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
                Id = mcm.Movie!.Id,
                Title = mcm.Movie.Title,
                StartYear = mcm.Movie.StartYear,
                Runtime = mcm.Movie.RuntimeMinutes,
                MovieRatingSummary = mcm.Movie.MovieRates.Any()
                ? new MovieRatingSummary
                {
                    AvgRating = mcm.Movie.MovieRates.Average(mr => mr.Rating),
                    RateCount = mcm.Movie.MovieRates.Count()
                }
                : new MovieRatingSummary { AvgRating = 0, RateCount = 0 }
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

    public async Task AddMovieToMovieCollection(int collectionId, int movieId, string userId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var movieExists = await context.Movies.AnyAsync(m => m.Id == movieId);
        if (!movieExists)
        {

            return;
        }

        var associationExists = await context.MovieCollectionMovies
            .AnyAsync(mcm => mcm.IdMovieCollection == collectionId && mcm.Movie_Id == movieId);

        if (associationExists)
        {
            return;
        }

        var collectionMovie = new MovieCollectionMovie
        {
            IdMovieCollection = collectionId,
            Movie_Id = movieId,
            AddedAt = DateTime.UtcNow
        };

        context.MovieCollectionMovies.Add(collectionMovie);
        await context.SaveChangesAsync();
    }

    public async Task RemoveMovieFromMyRatingsCollection(int movieId, string userId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var myRatingsCollection = await context.MovieCollections
            .FirstOrDefaultAsync(mc => mc.Type == CollectionType.MyRatings && mc.MovieCollectionUsers.Any(mcu => mcu.IdUser == userId));

        var movieToRemove = await context.MovieCollectionMovies
            .FirstOrDefaultAsync(mcm => mcm.IdMovieCollection == myRatingsCollection.Id && mcm.Movie_Id == movieId);

        if (movieToRemove != null)
        {
            context.MovieCollectionMovies.Remove(movieToRemove);
            await context.SaveChangesAsync();
        }
    }

    public async Task AddMovieToMyRatingsCollection(int movieId, string userId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var myRatingsCollection = await context.MovieCollections
            .FirstOrDefaultAsync(mc => mc.Type == CollectionType.MyRatings && mc.MovieCollectionUsers.Any(mcu => mcu.IdUser == userId));

        var movieExists = await context.MovieCollectionMovies
            .AnyAsync(mcm => mcm.IdMovieCollection == myRatingsCollection.Id && mcm.Movie_Id == movieId);

        if (movieExists)
        {
            return;
        }

        var collectionMovie = new MovieCollectionMovie
        {
            IdMovieCollection = myRatingsCollection.Id,
            Movie_Id = movieId,
            AddedAt = DateTime.UtcNow
        };
        context.MovieCollectionMovies.Add(collectionMovie);
        await context.SaveChangesAsync();
    }

    public async Task<int> GetMovieCountForCollection(int collectionId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.MovieCollectionMovies.CountAsync(m => m.IdMovieCollection == collectionId);
    }

    public async Task<Dictionary<ApplicationUser, CollectionRole>> GetCollectionMembers(int collectionId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.MovieCollectionUsers
            .Where(x => x.IdMovieCollection == collectionId)
            .Include(x => x.IdUserNavigation)
            .ToDictionaryAsync(x => x.IdUserNavigation, x => x.Role);
    }

    public async Task<CollectionRole?> GetUserCollectionRole(int collectionId, string userId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var userRole = await context.MovieCollectionUsers
            .FirstOrDefaultAsync(x => x.IdMovieCollection == collectionId && x.IdUser == userId);

        return userRole?.Role;
    }


    public async Task UpdateCollectionMembers(int collectionId, IDictionary<ApplicationUser, CollectionRole> membersRoles)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        var collectionUsers = await context.MovieCollectionUsers
            .Where(x => x.IdMovieCollection == collectionId)
            .ToListAsync();

        var existingUserIds = collectionUsers.Select(u => u.IdUser).ToHashSet();
        var incomingUserIds = membersRoles.Keys.Select(u => u.Id).ToHashSet();

        var usersToRemove = collectionUsers.Where(cu => !incomingUserIds.Contains(cu.IdUser)).ToList();
        if (usersToRemove.Any())
        {
            context.MovieCollectionUsers.RemoveRange(usersToRemove);
        }

        var usersToAddIds = incomingUserIds.Where(id => !existingUserIds.Contains(id)).ToList();
        if (usersToAddIds.Any())
        {
            var usersToAdd = membersRoles
                .Where(mr => usersToAddIds.Contains(mr.Key.Id))
                .Select(mr => new MovieCollectionUser
                {
                    IdMovieCollection = collectionId,
                    IdUser = mr.Key.Id,
                    Role = mr.Value
                });
            await context.MovieCollectionUsers.AddRangeAsync(usersToAdd);
        }

        var usersToUpdate = collectionUsers.Where(cu => incomingUserIds.Contains(cu.IdUser)).ToList();
        foreach (var user in usersToUpdate)
        {
            var appUser = membersRoles.Keys.First(x => x.Id == user.IdUser);
            if (user.Role != membersRoles[appUser])
            {
                user.Role = membersRoles[appUser];
            }
        }

        await context.SaveChangesAsync();
    }

    public async Task AddMemberToCollectionAsync(int collectionId, string userId, CollectionRole role)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        var newMember = new MovieCollectionUser
        {
            IdMovieCollection = collectionId,
            IdUser = userId,
            Role = role
        };
        context.MovieCollectionUsers.Add(newMember);
        await context.SaveChangesAsync();
    }

    public async Task<List<MovieCollection>> GetUserCollectionsContainingMovie(string userId, int movieId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        var collections = await context.MovieCollectionMovies
            .Where(mcm => mcm.Movie_Id == movieId && mcm.IdMovieCollectionNavigation.MovieCollectionUsers.Any(mcu => mcu.IdUser == userId))
            .Select(mcm => mcm.IdMovieCollectionNavigation)
            .ToListAsync();

        return collections;
    }

    public async Task LeaveCollectionAsync(int collectionId, string userId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        var collectionUser = await context.MovieCollectionUsers
            .FirstOrDefaultAsync(x => x.IdMovieCollection == collectionId && x.IdUser == userId);

        if (collectionUser != null)
        {
            context.MovieCollectionUsers.Remove(collectionUser);
            await context.SaveChangesAsync();
        }
    }

    public async Task<List<GetMovieCollectionWithDetails>> GetCommunityMovieCollections()
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var movieCollections = await context.MovieCollections
            .Where(x => x.Visibility == CollectionVisibility.Public)
            .OrderBy(m => m.MovieCollectionUsers.Count)
            .Include(mc => mc.MovieCollectionUsers)
            .ThenInclude(mcu => mcu.IdUserNavigation)
            .ToListAsync();

        var result = new List<GetMovieCollectionWithDetails>();

        foreach (var mc in movieCollections)
        {
            var movieCount = await context.MovieCollectionMovies.CountAsync(mcm => mcm.IdMovieCollection == mc.Id);

            result.Add(new GetMovieCollectionWithDetails
            {
                Id = mc.Id,
                Name = mc.Name,
                Description = mc.Description,
                CreatedAt = mc.CreatedAt,
                Type = mc.Type,
                Visibility = mc.Visibility,
                Members = mc.MovieCollectionUsers.ToDictionary(mcu => mcu.IdUserNavigation, mcu => mcu.Role),
                MovieCount = movieCount
            });
        }

        return result;
    }
}