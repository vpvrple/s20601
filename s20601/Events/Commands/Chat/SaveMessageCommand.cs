using MediatR;

namespace s20601.Events.Commands.Chat;

public record SaveMessageCommand(
    string IdRecipient,
    string message
) : INotification;