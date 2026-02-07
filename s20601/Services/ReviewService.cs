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
    private readonly ICurrentUserService _currentUserService;

    public ReviewService(IDbContextFactory<ApplicationDbContext> dbContextFactory, IMediator mediator, ICurrentUserService currentUserService)
    {
        _dbContextFactory = dbContextFactory;
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    public async Task<GetMovieReviewWithRating> GetMovieReviewWithRatingById(int id)
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

    public async Task AddReview(string content, int movieId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var authenticatedUserId = await _currentUserService.GetAuthenticatedUserId();

        if (authenticatedUserId is null)
            return;

        var review = new Review
        {
            Content = content,
            CreatedAt = DateTime.UtcNow,
            Movie_Id = movieId,
            IdAuthor = authenticatedUserId
        };
        context.Reviews.Add(review);

        await context.SaveChangesAsync();
    }

    public async Task RemoveReview(int reviewId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var review = await context.Reviews
            .Include(x => x.ReviewRates)
            .Where(x => x.Id == reviewId)
            .FirstOrDefaultAsync();

        var authenticatedUserId = await _currentUserService.GetAuthenticatedUserId();

        if (review == null)
            throw new ArgumentNullException("Review not found.");
        if (review.IdAuthor != authenticatedUserId)
            throw new UnauthorizedAccessException("Not authorized.");

        await _mediator.Publish(new ReviewRemovedCommand(authenticatedUserId, reviewId, 0));

        context.ReviewRates.RemoveRange(review.ReviewRates);
        context.Reviews.Remove(review);
        await context.SaveChangesAsync();
    }


    public async Task<bool> AlreadyReviewed(int movieId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        var authenticatedUserId = await _currentUserService.GetAuthenticatedUserId();
        return await context.Reviews
            .AnyAsync(x => x.Movie_Id == movieId && x.IdAuthor == authenticatedUserId);
    }


    public async Task VoteReview(int reviewId, ReviewRateType? vote)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var review = await context.Reviews.FindAsync(reviewId);
        if (review is null) return;

        var authenticatedUserId = await _currentUserService.GetAuthenticatedUserId();

        if (authenticatedUserId is null)
        {
            return;
        }

        var authorId = review.IdAuthor;

        var currentVote = await context.ReviewRates
            .Where(x => x.Review_Id == reviewId && x.IdUser == authenticatedUserId)
            .FirstOrDefaultAsync();

        if (vote is null)
        {
            if (currentVote is not null)
            {
                context.ReviewRates.Remove(currentVote);
                if (authenticatedUserId != authorId)
                    await _mediator.Publish(new ReviewUnratedCommand(authorId, currentVote.ReviewRateType, 1));
            }
        }
        else
        {
            if (currentVote is null)
            {
                context.ReviewRates.Add(new ReviewRate
                {
                    IdUser = authenticatedUserId,
                    Review_Id = reviewId,
                    ReviewRateType = vote.Value,
                    RatedAt = DateTime.UtcNow
                });

                if (vote.Value == ReviewRateType.Like)
                {
                    if (authenticatedUserId != authorId)
                        await _mediator.Publish(new ReviewLikedCommand(authorId, 1));
                }
                else
                {
                    if (authenticatedUserId != authorId)
                        await _mediator.Publish(new ReviewDislikedCommand(authorId, 1));
                }
            }
            else if (currentVote.ReviewRateType != vote.Value)
            {
                if (authenticatedUserId != authorId)
                    await _mediator.Publish(new ReviewUnratedCommand(authorId, currentVote.ReviewRateType, 1));

                currentVote.ReviewRateType = vote.Value;
                currentVote.RatedAt = DateTime.UtcNow;
                context.ReviewRates.Update(currentVote);

                if (currentVote.ReviewRateType == ReviewRateType.Like)
                {
                    if (authenticatedUserId != authorId)
                        await _mediator.Publish(new ReviewLikedCommand(authorId, 1));
                }
                else if (currentVote.ReviewRateType == ReviewRateType.Dislike)
                {
                    if (authenticatedUserId != authorId)
                        await _mediator.Publish(new ReviewDislikedCommand(authorId, 1));
                }
            }
        }

        await context.SaveChangesAsync();
    }

    public async Task<ReviewRateType?> GetUserVoteByReview(int reviewId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var authenticatedUserId = await _currentUserService.GetAuthenticatedUserId();

        return await context.ReviewRates
            .Where(x => x.IdUser == authenticatedUserId && x.Review_Id == reviewId)
            .Select(x => (ReviewRateType?)x.ReviewRateType)
            .FirstOrDefaultAsync();
    }

    public async Task UpdateReview(int reviewId, string content)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var review = await context.Reviews
            .Include(x => x.ReviewRates)
            .Where(x => x.Id == reviewId)
            .FirstOrDefaultAsync();

        var authenticatedUserId = await _currentUserService.GetAuthenticatedUserId();


        if (review == null)
            throw new ArgumentNullException("Review not found.");
        if (review.IdAuthor != authenticatedUserId)
            throw new UnauthorizedAccessException("Not authorized.");

        // Reset points and ratings
        await _mediator.Publish(new ReviewRemovedCommand(authenticatedUserId, reviewId, 0));
        context.ReviewRates.RemoveRange(review.ReviewRates);

        review.Content = content;
        review.CreatedAt = DateTime.UtcNow;

        context.Reviews.Update(review);
        await context.SaveChangesAsync();
    }
}