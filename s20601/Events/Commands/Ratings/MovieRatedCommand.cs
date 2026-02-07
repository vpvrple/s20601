using MediatR;

namespace s20601.Events.Commands.Ratings;

public record MovieRatedCommand(int movieId, string userId) : INotification;