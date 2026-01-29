using s20601.Data.Models;
using s20601.Data.Models.DTOs;

namespace s20601.Services;

public interface IChatService
{
    Task<List<ChatMessage>?> GetConversation(string friendId);
    Task<Message?> SaveMessage(string idRecipient, string message);
}
