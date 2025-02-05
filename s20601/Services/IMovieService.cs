using s20601.Data.Models;

namespace s20601.Services
{
    public interface IMovieService
    {
        Task<MovieOfTheDay?> GetCurrentMovieOfTheDayAsync();
    }
}