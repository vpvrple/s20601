using MediatR;
using s20601.Data.Models;

namespace s20601.Events.Commands;

public record ReviewUnratedCommand(string userId, ReviewRateType vote, int points) : INotification;