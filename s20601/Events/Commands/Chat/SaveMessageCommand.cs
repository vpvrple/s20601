using MediatR;

namespace s20601.Events.Commands;

public record SaveMessageCommand(
    string IdRecipient, 
    string message
) : INotification;