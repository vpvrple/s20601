using MediatR;
using Microsoft.AspNetCore.SignalR;
using s20601.Data.Models;
using s20601.Events.Commands;
using s20601.Events.Commands.Chat;

namespace s20601.Hubs;

public class ChatHub : Hub
{
    private readonly IMediator _mediator;

    public ChatHub(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task SendMessage(string idRecipient, string message)
    {
        var relationship = await _mediator.Send(new SendMessageCommand(idRecipient));

        if (relationship != RelationshipType.Friends)
        {
            return;
        }

        await _mediator.Send(new SaveMessageCommand(idRecipient, message));

        await Clients.User(idRecipient).SendAsync("ReceiveMessage", Context.UserIdentifier, message);

        await Clients.Caller.SendAsync("ReceiveMessage", Context.UserIdentifier, message);
    }

    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var userId = httpContext.Request.Query["userId"];

        if (!string.IsNullOrEmpty(userId))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
        }

        await base.OnConnectedAsync();
    }
}