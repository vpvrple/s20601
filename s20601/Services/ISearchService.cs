using s20601.Data.Models;

namespace s20601.Services;

public interface ISearchService
{
    Task<List<Movie>> SearchMovieByTitle(string title);
}