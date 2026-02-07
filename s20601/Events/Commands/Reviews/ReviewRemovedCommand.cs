using MediatR;

namespace s20601.Events.Commands.Reviews;

public record ReviewRemovedCommand(string userId, int reviewId, int points) : INotification;