using s20601.Data.Models;

namespace s20601.Services;

public interface IUserService
{
    Task<ApplicationUser?> GetUserById(string id);
    Task<string?> GetUserIdByUsername(string username);
    Task<string?> GetUserAvatarByUserId(string userId);
    Task<string?> UpdateUserAvatar(Stream? fileStream, string? avatar);
    Task<string?> UpdateUserProfileDescription(string? profileDescription);
    Task DailyLogin();
}
