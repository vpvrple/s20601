using MediatR;
using s20601.Data.Models;
using s20601.Events.Commands;
using s20601.Services;

public class SaveMessageHandler : INotificationHandler<SaveMessageCommand>
{
    private readonly IChatService _chatService;

    public SaveMessageHandler(IChatService chatService)
    {
        _chatService = chatService;
    }

    public async Task Handle(SaveMessageCommand request, CancellationToken cancellationToken)
    {
        await _chatService.SaveMessage(request.IdRecipient, request.message);
    }
}