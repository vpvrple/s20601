using s20601.Data.Models;
using s20601.Data.Models.DTOs;

namespace s20601.Services;

public interface IReviewService
{
    Task<GetMovieReviewWithRating> GetMovieReviewWithRatingByIdAsync(int id);
    Task<List<GetMovieReviewWithRating>> GetMovieReviewsWithRating(int id);
    Task AddReviewAsync(string content, int movieId, string authorId);
    Task<bool> AlreadyReviewed(int movieId, string authorId);
    Task<GetMovieReviewWithRating> GetUserMovieReviewWithRating(string userId, int movieId);
    Task RemoveReviewAsync(int reviewId, string userId);
    Task VoteReview(int reviewId, string userId, ReviewRateType? vote);
    Task<ReviewRateType?> GetUserVoteByReview(int reviewId, string userId);
}
