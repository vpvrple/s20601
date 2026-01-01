using s20601.Data.Models;
using s20601.Data.Models.DTOs;

namespace s20601.Services;

public interface IRatingService
{
    Task RateMovieAsync(int movieId, int? rating);
    Task<UserRatingSummary?> GetUserRatingSummaryAsync();
    Task<MovieRatingSummary?> GetMovieRatingSummaryByIdAsync(int id);
    Task<MovieRate?> GetUserMovieRating(int movieId);
    Task<List<MovieWithRating>> GetTopMoviesByRatingAsync(int n);
}