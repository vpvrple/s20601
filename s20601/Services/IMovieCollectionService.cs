using s20601.Data.Models;
using s20601.Data.Models.DTOs;

namespace s20601.Services;

public interface IMovieCollectionService
{
    Task<List<MovieCollection>> GetUserMovieCollections(string userId);
    Task<MovieCollection> CreateMovieCollection(string name, string? description, string userId);
    Task DeleteMovieCollection(int collectionId);
    Task<MovieCollection?> GetMovieCollectionById(int collectionId);
    Task UpdateMovieCollection(MovieCollection collection);
    Task AddMovieToMovieCollection(int collectionId, int movieId, string userId);
    Task<List<MovieWithRating>> GetMoviesWithRatingOfMovieCollectionById(int collectionId);
    Task<int> GetMovieCountForCollection(int collectionId);
}
