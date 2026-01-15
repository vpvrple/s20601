using MediatR;

namespace s20601.Events.Commands;

public record ReviewLikedCommand(string userId, int points) : INotification;