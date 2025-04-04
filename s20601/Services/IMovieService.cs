using s20601.Data.Models;
using s20601.Data.Models.DTOs;

namespace s20601.Services
{
    public interface IMovieService
    {
        Task<Movie?> GetMovieOfTheDayAsync();
        Task<Movie?> GetMovieByIdAsync(int id);
        Task<List<Genre>> GetMovieGenresByIdAsync(int id);
        Task<List<GetMovieCrewMemberWithDetails>> GetMovieCrewByMovieIdAsync(int id);
        Task<List<GetMovieReviewWithRating>> GetMovieReviewsByMovieIdAsync(int id);
        Task<List<GetMovieReviewWithRating>> GetMovieReviewsByMovieIdAsync(int id, int n);
        Task<List<Movie>> GetPastMoviesOfTheDay(int n);
        Task<List<MovieCollection>> GetTrendingMovieCollections(int n);
        //Task<List<MovieWithRating>> GetTopMoviesByRatingAsync(int n);
    }
}