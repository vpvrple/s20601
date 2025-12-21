using s20601.Data.Models;

namespace s20601.Services;

public interface IChatService
{
    Task<List<Message>> GetConversation(string userId1, string userId2);
    Task<Message> SaveMessage(string IdSender, string IdRecipient, string message);
}
