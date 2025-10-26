using s20601.Data.Models;

namespace s20601.Services;

public interface IMovieCollectionService
{
    Task<List<MovieCollection>> GetUserMovieCollections(string userId);
}
