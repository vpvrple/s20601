using MediatR;
using s20601.Services;

namespace s20601.Events.Commands;

public class ReviewRemovedEventHandler
    : INotificationHandler<ReviewRemovedCommand>

{
    private readonly IRankingService _rankingService;
    private readonly IReviewService _reviewService;

    public ReviewRemovedEventHandler(IRankingService rankingService, IReviewService reviewService)
    {
        _rankingService = rankingService;
        _reviewService = reviewService;
    }

    public async Task Handle(ReviewRemovedCommand command, CancellationToken cancellationToken)
    {
        var review = await _reviewService.GetMovieReviewWithRatingByIdAsync(command.reviewId);
        if (review == null)
        {
            return;
        }

        var userPoints = await _rankingService.GetUserPointsById(command.userId);
        var likesCount = review.LikeRating;

        var pointsToDeduct = likesCount;

        if (userPoints - likesCount < 1)
        {
            pointsToDeduct = userPoints > 1 ? userPoints - 1 : 0;
        }

        if (pointsToDeduct > 0)
        {
            await _rankingService.DecrementPoints(command.userId, pointsToDeduct);
        }

        if (command.points > 0)
        {
            await _rankingService.DecrementPoints(command.userId, command.points);
        }
    }
}