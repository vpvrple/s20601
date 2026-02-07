using MediatR;

namespace s20601.Events.Commands.MovieRequests;

public record MovieRequestApprovedCommand(string userId, int points) : INotification;