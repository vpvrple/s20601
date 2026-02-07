using MediatR;
using s20601.Services;

namespace s20601.Events.Commands.Reviews;

public class ReviewDislikedEventHandler
    : INotificationHandler<ReviewDislikedCommand>

{
    private readonly IRankingService _rankingService;

    public ReviewDislikedEventHandler(IRankingService rankingService)
    {
        _rankingService = rankingService;
    }

    public async Task Handle(ReviewDislikedCommand command, CancellationToken cancellationToken)
    {
        await _rankingService.DecrementPoints(command.userId, command.points);
    }
}