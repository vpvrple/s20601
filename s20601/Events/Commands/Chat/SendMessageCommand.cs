using Azure;
using MediatR;
using s20601.Services.External.Azure;

namespace s20601.Events.Commands;

public record SendMessageCommand(string IdSender, 
    string IdRecipient, 
    string message
) : INotification, IRequest<Response>;