using MediatR;

namespace s20601.Events.Commands.Reviews;

public record ReviewLikedCommand(string userId, int points) : INotification;