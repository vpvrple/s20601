using s20601.Data;
using s20601.Data.Models;
using s20601.Data.Models.DTOs;

namespace s20601.Services;

public interface IUserService
{
    Task<ApplicationUser?> GetUserByUsernameAsync(string username);
    Task<List<ApplicationUser>> GetTopUsersByPoints();
}