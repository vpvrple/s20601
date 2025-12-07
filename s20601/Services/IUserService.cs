using s20601.Data.Models;

namespace s20601.Services;
public interface IUserService
{
    Task<ApplicationUser?> GetUserByUsernameAsync(string username);
    Task<ApplicationUser?> GetUserByIdAsync(string id);
}
