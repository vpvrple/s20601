using System.Security.Claims;
using s20601.Data.Models;

namespace s20601.Services;
public interface IUserService
{
    Task<ApplicationUser?> GetUserByUsername(string username);
    // Task<ApplicationUser?> GetUserByIdAsync(string id);

    Task<string?> GetAuthenticatedUserId();
    Task<ClaimsPrincipal?> GetAuthenticatedUser();
    Task<string?> GetUserAvatarByUserId(string userId);
    Task<string?> UpdateUserProfileDescription(string? profileDescription);
    Task<string?> UpdateUserAvatar(Stream? fileStream, string? avatar);
}
