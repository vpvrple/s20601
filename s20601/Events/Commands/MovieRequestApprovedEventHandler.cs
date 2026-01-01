using MediatR;
using s20601.Data.Models;
using s20601.Services;

namespace s20601.Events.Commands;

public class MovieRequestApprovedEventHandler
    : INotificationHandler<MovieRequestApprovedCommand>

{
    private readonly IRankingService _rankingService;

    public MovieRequestApprovedEventHandler(IRankingService rankingService)
    {
        _rankingService = rankingService;
    }

    public async Task Handle(MovieRequestApprovedCommand command, CancellationToken cancellationToken)
    {
        await _rankingService.IncrementPoints(command.userId, command.points);
    }
}