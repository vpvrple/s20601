using s20601.Data.Models;
using s20601.Data.Models.DTOs;

namespace s20601.Services;

public interface IUserService
{
    Task<User?> GetUserByIdAsync(int id);
    Task<UserRatingSummary?> GetUserRatingSummaryAsync(int id);
    Task<List<User>> GetTopUsersByPoints();
}