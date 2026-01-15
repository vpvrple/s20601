using MediatR;

namespace s20601.Events.Commands;

public record MovieRequestApprovedCommand(string userId, int points) : INotification;