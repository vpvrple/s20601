using MediatR;
using s20601.Data.Models;

namespace s20601.Events.Commands.Reviews;

public record ReviewUnratedCommand(string userId, ReviewRateType vote, int points) : INotification;