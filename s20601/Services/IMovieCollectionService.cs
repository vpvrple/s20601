using System.Linq.Expressions;
using s20601.Data.Models;
using s20601.Data.Models.DTOs;

namespace s20601.Services;

public interface IMovieCollectionService
{
    Task<List<MovieCollection>> GetUserMovieCollections(string userId);
    Task<GetMovieCollectionWithDetails> CreateMovieCollection(string name, string? description, string userId, CollectionVisibility visibility);
    Task DeleteMovieCollection(int collectionId);
    Task<MovieCollection?> GetMovieCollectionById(int collectionId);
    Task UpdateMovieCollection(MovieCollection collection);
    Task AddMovieToMovieCollection(int collectionId, int movieId, string userId);
    Task<List<MovieWithRating>> GetMoviesWithRatingOfMovieCollectionById(int collectionId);
    Task<int> GetMovieCountForCollection(int collectionId);
    Task DeleteMoviesFromMovieCollection(IEnumerable<int> movieIds, int collectionId);
    Task<List<MovieCollection>> GetNRecentUserMovieColletions(string userId, int n);
    Task RemoveMovieFromMyRatingsCollection(int movieId, string userId);
    Task AddMovieToMyRatingsCollection(int movieId, string userId);
    Task<List<GetMovieCollectionWithDetails>> GetMovieCollectionsWithDetails(Expression<Func<MovieCollection, bool>>? filter = null);
    Task<Dictionary<ApplicationUser, CollectionRole>> GetCollectionMembers(int collectionId);
    Task UpdateCollectionMembers(int collectionId, IDictionary<ApplicationUser, CollectionRole> membersRoles);
    Task AddMemberToCollectionAsync(int collectionId, string userId, CollectionRole role);
    Task<CollectionRole?> GetUserCollectionRole(int collectionId, string userId);
    Task<List<MovieCollection>> GetUserCollectionsContainingMovie(string userId, int movieId);
    Task LeaveCollectionAsync(int collectionId, string userId);
    Task<List<MovieCollection>> GetTrendingMovieCollections(int n);

}
