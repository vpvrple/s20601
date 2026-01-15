using s20601.Data.Models;
using s20601.Data.Models.DTOs;
using s20601.Services.External.Azure;
using System.Linq.Expressions;

namespace s20601.Services;

public interface IMovieService
{
    Task<Movie?> GetMovieOfTheDayAsync();
    Task<Movie?> GetMovieByIdAsync(int id);
    Task<List<Genre>> GetMovieGenresByIdAsync(int id);
    Task<List<Genre>> GetAllGenresAsync();
    Task<List<GetMovieCrewMemberWithDetails>> GetMovieCrewByMovieIdAsync(int id);
    Task<List<Movie>> GetPastMoviesOfTheDay(int n);
    Task<List<Movie>> GetTrendingMovies(int n);
    Task<MovieWithRating?> GetMovieWithRatingById(int id);

    Task AddMovieUpdateRequest(MovieUpdateRequest request);
    Task<MovieUpdateRequest?> GetMovieUpdateRequest(int id);
    Task<List<GetMovieCrewMemberWithDetails>> SearchCrewAsync(string query, int? movieId = null);
    Task<List<MovieUpdateRequest>> GetMovieUpdateRequests(Expression<Func<MovieUpdateRequest, bool>>? predicate = null);
    
    Task ApproveMovieUpdateRequest(int requestId);
    Task RejectMovieUpdateRequest(int requestId);

    Task<string?> GetMoviePosterByMovieId(int id);
    Task<string?> GetMoviePoster(string posterPath);
    Task<string?> GetMovieOverviewById(int id);

    Task<string?> UploadMoviePoster(int movieId, Stream fileStream, string fileName);

    Task<SentimentType> GetMovieSentimentByMovieId(int Id);
}
