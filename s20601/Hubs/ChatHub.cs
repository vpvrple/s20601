using Microsoft.AspNetCore.SignalR;
using s20601.Services;

namespace s20601.Hubs;

public class ChatHub : Hub
{
    private readonly IChatService _chatService;

    public ChatHub(IChatService chatService)
    {
        _chatService = chatService;
    }

    public async Task SendMessage(string idRecipient, string message)
    {
        var savedMessage = await _chatService.SaveMessage(idRecipient, message);

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