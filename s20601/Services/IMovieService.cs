using s20601.Data.Models;
using s20601.Data.Models.DTOs;

namespace s20601.Services
{
    public interface IMovieService
    {
        Task<Movie?> GetMovieOfTheDayAsync();
        Task<Movie?> GetMovieByIdAsync(string id);
        Task<List<Genre>> GetMovieGenresByIdAsync(string id);
        Task<List<GetMovieCrewMemberWithDetails>> GetMovieCrewByMovieIdAsync(string id);
        Task<List<GetMovieReviewWithRating>> GetMovieReviewsByMovieIdAsync(string id);
        Task<List<GetMovieReviewWithRating>> GetMovieReviewsByMovieIdAsync(string id, int n);
        Task<List<Movie>> GetPastMoviesOfTheDay(int n);
        Task<List<MovieCollection>> GetTrendingMovieCollections(int n);
        Task<List<MovieWithRating>> GetTopMoviesByRatingAsync(int n);
        Task<MovieRatingSummary?> GetMovieRatingSummaryByIdAsync(string id);
    }
}