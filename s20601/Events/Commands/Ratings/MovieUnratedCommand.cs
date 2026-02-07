using MediatR;

namespace s20601.Events.Commands.Ratings;

public record MovieUnratedCommand(int movieId, string userId) : INotification;