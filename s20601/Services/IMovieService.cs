using s20601.Data.Models;
using s20601.Data.Models.DTOs;

namespace s20601.Services
{
    public interface IMovieService
    {
        Task<MovieOfTheDay?> GetCurrentMovieOfTheDayAsync();
        Task<int> GetMovieRatingByIdAsync(string id);
        Task<Movie?> GetMovieDataByIdAsync(string id);
        Task<List<Genre>> GetMovieGenresByIdAsync(string id);
        Task<List<GetMovieCrewMemberWithDetails>> GetMovieCrewByMovieIdAsync(string id);
        Task<List<GetMovieReviewWithRating>> GetMovieReviewsByMovieIdAsync(string id);
        Task<List<GetMovieReviewWithRating>> GetMovieReviewsByMovieIdAsync(string id, int n);
    }
}