using MediatR;
using s20601.Data.Models;
using s20601.Services;

namespace s20601.Events.Commands.Reviews;

public class ReviewUnratedEventHandler
    : INotificationHandler<ReviewUnratedCommand>

{
    private readonly IRankingService _rankingService;

    public ReviewUnratedEventHandler(IRankingService rankingService)
    {
        _rankingService = rankingService;
    }

    public async Task Handle(ReviewUnratedCommand command, CancellationToken cancellationToken)
    {
        if (command.vote == ReviewRateType.Like)
        {
            await _rankingService.DecrementPoints(command.userId, command.points);
        }
        else if (command.vote == ReviewRateType.Dislike)
        {
            await _rankingService.IncrementPoints(command.userId, command.points);
        }

    }
}