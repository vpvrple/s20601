using MediatR;
using s20601.Data.Models;

namespace s20601.Events.Commands;

public record SendMessageCommand(string IdSender, 
    string IdRecipient, 
    string message
) : INotification, IRequest<RelationshipType?>;