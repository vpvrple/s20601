using Azure;
using MediatR;
using s20601.Data.Models;
using s20601.Events.Commands;
using s20601.Services;

public class SendMessageHandler : IRequestHandler<SendMessageCommand, RelationshipType?>
{
    private readonly IFriendService _friendService;

    public SendMessageHandler(IFriendService friendService)
    {
        _friendService = friendService;
    }

    public async Task<RelationshipType?> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        return await _friendService.GetUsersRelationshipType(request.IdRecipient);
    }
}