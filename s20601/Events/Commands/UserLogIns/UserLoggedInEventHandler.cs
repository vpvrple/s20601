using MediatR;
using s20601.Services;

namespace s20601.Events.Commands.UserLogIns;

public class UserLoggedInEventHandler
    : INotificationHandler<UserLoggedInCommand>

{
    private readonly IRankingService _rankingService;

    public UserLoggedInEventHandler(IRankingService rankingService)
    {
        _rankingService = rankingService;
    }

    public async Task Handle(UserLoggedInCommand command, CancellationToken cancellationToken)
    {
        await _rankingService.IncrementPoints(command.userId, command.points);
    }
}