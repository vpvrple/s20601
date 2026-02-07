using Azure;
using MediatR;
using s20601.Data.Models;
using s20601.Events.Commands;
using s20601.Services;
using s20601.Services.External.Azure;

public class SendMessageHandler : IRequestHandler<SendMessageCommand, RelationshipType>
{
    private readonly IFriendService _friendService;

    public SendMessageHandler()
    {
    }

    public async Task<Response> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        return await _friendService.GetUsersRelationshipType(request.IdRecipient);
    }
}