using Microsoft.AspNetCore.Authorization;

namespace s20601.Components.Account.Policies;

public class MinimumPointsRequirement : IAuthorizationRequirement
{
    public MinimumPointsRequirement(int minimumPoints) =>
        MinimumPoints = minimumPoints;

    public int MinimumPoints { get; }
}