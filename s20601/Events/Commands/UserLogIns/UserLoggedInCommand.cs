using MediatR;

namespace s20601.Events.Commands.UserLogIns;

public record UserLoggedInCommand(string userId, int points) : INotification;