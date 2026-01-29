using s20601.Data.Models;
using s20601.Data.Models.DTOs;

namespace s20601.Services;

public interface IReviewService
{
    Task<GetMovieReviewWithRating> GetMovieReviewWithRatingById(int id);
    Task<List<GetMovieReviewWithRating>> GetMovieReviewsWithRating(int id);
    Task AddReview(string content, int movieId);
    Task<bool> AlreadyReviewed(int movieId);
    Task RemoveReview(int reviewId);
    Task VoteReview(int reviewId, ReviewRateType? vote);
    Task<ReviewRateType?> GetUserVoteByReview(int reviewId);
    Task UpdateReview(int reviewId, string content);
}
