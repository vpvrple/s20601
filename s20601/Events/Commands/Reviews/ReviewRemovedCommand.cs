using MediatR;

namespace s20601.Events.Commands;

public record ReviewRemovedCommand(string userId, int reviewId, int points) : INotification;