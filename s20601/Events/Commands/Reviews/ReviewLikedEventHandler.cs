using MediatR;
using s20601.Services;

namespace s20601.Events.Commands.Reviews;

public class ReviewLikedEventHandler
    : INotificationHandler<ReviewLikedCommand>

{
    private readonly IRankingService _rankingService;

    public ReviewLikedEventHandler(IRankingService rankingService)
    {
        _rankingService = rankingService;
    }

    public async Task Handle(ReviewLikedCommand command, CancellationToken cancellationToken)
    {
        await _rankingService.IncrementPoints(command.userId, command.points);
    }
}