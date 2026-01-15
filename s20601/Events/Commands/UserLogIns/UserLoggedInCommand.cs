using MediatR;

namespace s20601.Events.Commands;

public record UserLoggedInCommand(string userId, int points) : INotification;