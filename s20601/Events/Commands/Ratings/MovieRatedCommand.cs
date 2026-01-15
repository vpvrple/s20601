using MediatR;

namespace s20601.Events.Commands;

public record MovieRatedCommand(int movieId, string userId) : INotification;