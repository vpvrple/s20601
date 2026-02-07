using MediatR;

namespace s20601.Events.Commands.Reviews;

public record ReviewDislikedCommand(string userId, int points) : INotification;