using s20601.Data.Models;
using s20601.Data.Models.DTOs;

namespace s20601.Services
{
    public interface IRatingService
    {
        Task RateMovieAsync(int movieId, string username, int rating);
        Task<UserRatingSummary?> GetUserRatingSummaryAsync(string username);
        Task<MovieRatingSummary?> GetMovieRatingSummaryByIdAsync(int id);
        Task<MovieRate> GetUserMovieRating(string userId, int movieId);
    }
}