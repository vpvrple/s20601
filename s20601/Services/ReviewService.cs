using Microsoft.EntityFrameworkCore;
using s20601.Data;
using s20601.Data.Models;
using s20601.Data.Models.DTOs;

namespace s20601.Services;

public class ReviewService : IReviewService
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    public ReviewService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<GetMovieReviewWithRating> GetMovieReviewWithRatingByIdAsync(int id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var review = await context.Reviews
            .Where(x => x.Movie_Id == id)
            .Include(x => x.ReviewRates)
            .ThenInclude(x => x.IdUserNavigation)
            .Select(x => new GetMovieReviewWithRating
            {
                Id = x.Id,
                Nickname = x.IdAuthorNavigation.Nickname,
                CreatedAt = x.CreatedAt,
                Content = x.Content,
                LikeRating = x.ReviewRates.Select(x => x.Rating).Count(x => x == 1),
                DislikeRating = x.ReviewRates.Select(x => x.Rating).Count(x => x == -1)
            })
            .FirstOrDefaultAsync();

        return review;
    }

    public async Task<List<GetMovieReviewWithRating>> GetMovieReviewsWithRating(int id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var reviews = await context.Reviews
            .Where(x => x.Movie_Id == id)
            .Include(x => x.ReviewRates)
            .ThenInclude(x => x.IdUserNavigation)
            .Select(x => new GetMovieReviewWithRating
            {
                Id = x.Id,
                AuthorId = x.IdAuthor,
                Nickname = x.IdAuthorNavigation.Nickname,
                CreatedAt = x.CreatedAt,
                Content = x.Content,
                LikeRating = x.ReviewRates.Select(x => x.Rating).Count(x => x == 1),
                DislikeRating = x.ReviewRates.Select(x => x.Rating).Count(x => x == -1)
            })
            .ToListAsync();

        return reviews;
    }

    public async Task<GetMovieReviewWithRating> GetUserMovieReviewWithRating(string userId, int movieId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var review = await context.Reviews
            .Where(x => x.Movie_Id == movieId && x.IdAuthor == userId)
            .Include(x => x.ReviewRates)
            .ThenInclude(x => x.IdUserNavigation)
            .Select(x => new GetMovieReviewWithRating
            {
                Id = x.Id,
                AuthorId = x.IdAuthor,
                Nickname = x.IdAuthorNavigation.Nickname,
                CreatedAt = x.CreatedAt,
                Content = x.Content,
                LikeRating = x.ReviewRates.Select(x => x.Rating).Count(x => x == 1),
                DislikeRating = x.ReviewRates.Select(x => x.Rating).Count(x => x == -1)
            })
            .FirstOrDefaultAsync();

        return review;
    }

    public async Task AddReviewAsync(string content, int movieId, string authorId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var review = new Review
        {
            Content = content,
            CreatedAt = DateTime.UtcNow,
            Movie_Id = movieId,
            IdAuthor = authorId
        };
        context.Reviews.Add(review);

        await context.SaveChangesAsync();
    }

    public async Task RemoveReviewAsync(int reviewId, string userId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var review = await context.Reviews
            .Include(x => x.ReviewRates)
            .Where(x => x.Id == reviewId)
            .FirstOrDefaultAsync();

        if (review == null)
            throw new ArgumentNullException("Review not found.");
        else if (review.IdAuthor != userId)
            throw new UnauthorizedAccessException("Not authorized.");

        context.ReviewRates.RemoveRange(review.ReviewRates);
        context.Reviews.Remove(review);
        
        await context.SaveChangesAsync();
    }



    public async Task<bool> AlreadyReviewed(int movieId, string authorId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.Reviews
            .AnyAsync(x => x.Movie_Id == movieId && x.IdAuthor == authorId);

    }


    public async Task VoteReview(int reviewId, string userId, int vote)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var currentVote = await context.ReviewRates
            .Where(x => x.Review_Id == reviewId && x.IdUser == userId)
            .FirstOrDefaultAsync();

        if (currentVote is null)
        {
            context?.ReviewRates.Add(new ReviewRate
            {
                IdUser = userId,
                Review_Id = reviewId,
                Rating = vote,
                RatedAt = DateTime.UtcNow
            });
        }
        else
        {
            if (currentVote.Rating != vote)
            {
                currentVote.Rating = vote;
                context.ReviewRates.Update(currentVote);
            }
        }

        
        await context.SaveChangesAsync();
    }

    public async Task RemoveVote(int reviewId, string userId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        var vote = await context.ReviewRates
            .Where(x => x.Review_Id == reviewId && x.IdUser == userId)
            .FirstOrDefaultAsync();
        if (vote is not null)
        {
            context.ReviewRates.Remove(vote);
            await context.SaveChangesAsync();
        }
        
        
    }

    public async Task<ReviewRate> GetUserVoteByReview(int reviewId, string userId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.ReviewRates
            .FirstOrDefaultAsync(x => x.IdUser == userId && x.Review_Id == reviewId);

    }
}
