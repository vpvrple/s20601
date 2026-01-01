using Microsoft.AspNetCore.Authorization;

namespace s20601.Components.Account.Policies;

public class ReviewFrequencyRequirement : IAuthorizationRequirement
{
    public int MinPoints { get; }
    public int MaxPoints { get; }
    public TimeSpan TimeWindow { get; }

    public ReviewFrequencyRequirement(int minPoints, int maxPoints, TimeSpan timeWindow)
    {
        MinPoints = minPoints;
        MaxPoints = maxPoints;
        TimeWindow = timeWindow;
    }
}