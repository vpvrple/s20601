using MediatR;
using s20601.Data.Models;

namespace s20601.Events.Commands.Chat;

public record SendMessageCommand(string IdRecipient) : INotification, IRequest<RelationshipType?>;