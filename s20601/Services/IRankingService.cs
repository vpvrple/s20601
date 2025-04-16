using s20601.Data.Models;

namespace s20601.Services;

public interface IRankingService
{
    Task<List<ApplicationUser>> GetTopUsersByPoints();
}
