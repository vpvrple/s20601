using s20601.Data.Models;

namespace s20601.Services;

public interface IChatService
{
    Task<List<Message>?> GetConversation(string friendId);
    Task<Message?> SaveMessage(string idRecipient, string message);
}
