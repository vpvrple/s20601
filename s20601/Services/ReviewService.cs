using MediatR;
using Microsoft.EntityFrameworkCore;
using s20601.Data;
using s20601.Data.Models;
using s20601.Data.Models.DTOs;
using s20601.Events.Commands;

namespace s20601.Services;

public class ReviewService : IReviewService
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IMediator _mediator;
    public ReviewService(IDbContextFactory<ApplicationDbContext> dbContextFactory, IMediator mediator)
    {
        _dbContextFactory = dbContextFactory;
        _mediator = mediator;
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
                Username = x.IdAuthorNavigation.UserName,
                CreatedAt = x.CreatedAt,
                Content = x.Content,
                LikeRating = x.ReviewRates.Select(x => x.ReviewRateType).Count(rateType => rateType == ReviewRateType.Like),
                DislikeRating = x.ReviewRates.Select(x => x.ReviewRateType).Count(rateType => rateType == ReviewRateType.Dislike)
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
                Username = x.IdAuthorNavigation.UserName,
                CreatedAt = x.CreatedAt,
                Content = x.Content,
                LikeRating = x.ReviewRates.Select(x => x.ReviewRateType).Count(rateType => rateType == ReviewRateType.Like),
                DislikeRating = x.ReviewRates.Select(x => x.ReviewRateType).Count(rateType => rateType == ReviewRateType.Dislike)
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
                Username = x.IdAuthorNavigation.UserName,
                CreatedAt = x.CreatedAt,
                Content = x.Content,
                LikeRating = x.ReviewRates.Select(x => x.ReviewRateType).Count(rateType => rateType == ReviewRateType.Like),
                DislikeRating = x.ReviewRates.Select(x => x.ReviewRateType).Count(rateType => rateType == ReviewRateType.Dislike)
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


    public async Task VoteReview(int reviewId, string userId, ReviewRateType? vote)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var currentVote = await context.ReviewRates
            .Where(x => x.Review_Id == reviewId && x.IdUser == userId)
            .FirstOrDefaultAsync();

        if (vote is null)
        {
            if (currentVote is not null)
            {
                context.ReviewRates.Remove(currentVote);
                await _mediator.Publish(new ReviewUnratedCommand(userId, currentVote.ReviewRateType, 1));
            }
        }
        else
        {
            if (currentVote is null)
            {
                context.ReviewRates.Add(new ReviewRate
                {
                    IdUser = userId,
                    Review_Id = reviewId,
                    ReviewRateType = vote.Value,
                    RatedAt = DateTime.UtcNow
                });
                await _mediator.Publish(new ReviewLikedCommand(userId, 1));
            }
            else if (currentVote.ReviewRateType != vote.Value)
            {
                currentVote.ReviewRateType = vote.Value;
                currentVote.RatedAt = DateTime.UtcNow;
                context.ReviewRates.Update(currentVote);
                if (currentVote.ReviewRateType == ReviewRateType.Like)
                {
                    await _mediator.Publish(new ReviewLikedCommand(userId, 1));
                }
                else if (currentVote.ReviewRateType == ReviewRateType.Dislike)
                {
                    await _mediator.Publish(new ReviewDislikedCommand(userId, -1));
                }
            }
        }

        await context.SaveChangesAsync();
    }

    // public async Task RemoveVote(int reviewId, string userId)
    // {
    //     using var context = await _dbContextFactory.CreateDbContextAsync();
    //     var vote = await context.ReviewRates
    //         .Where(x => x.Review_Id == reviewId && x.IdUser == userId)
    //         .FirstOrDefaultAsync();
    //     if (vote is not null)
    //     {
    //         context.ReviewRates.Remove(vote);
    //         await context.SaveChangesAsync();
    //     }
    // }

    public async Task<ReviewRateType?> GetUserVoteByReview(int reviewId, string userId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.ReviewRates
            .Where(x => x.IdUser == userId && x.Review_Id == reviewId)
            .Select(x => (ReviewRateType?)x.ReviewRateType)
            .FirstOrDefaultAsync();
    }
}
