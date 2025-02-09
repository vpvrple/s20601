using s20601.Data.Models;

namespace s20601.Services
{
    public interface IMovieService
    {
        Task<MovieOfTheDay?> GetCurrentMovieOfTheDayAsync();
        Task<int> GetMovieRatingByIdAsync(string id);
        Task<Movie?> GetMovieDataByIdAsync(string id);
    }
}