using s20601.Data.Models;

namespace s20601.Services;
public interface IUserService
{
    Task<ApplicationUser?> GetUserByNicknameAsync(string nickname);
    Task<ApplicationUser?> GetUserByIdAsync(string id);
}