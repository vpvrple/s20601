using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using s20601.Data;

namespace s20601.Components.Account.Policies;

public class ReviewFrequencyHandler : AuthorizationHandler<ReviewFrequencyRequirement>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public ReviewFrequencyHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, ReviewFrequencyRequirement requirement)
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            return;
        }

        using var dbContext = await _dbContextFactory.CreateDbContextAsync();
        var user = await dbContext.Users.FindAsync(userId);

        if (user == null)
        {
            return;
        }

        if (user.ReputationPoints >= requirement.MinPoints && user.ReputationPoints < requirement.MaxPoints)
        {
            var lastReview = await dbContext.Reviews
                .Where(r => r.IdAuthor == userId)
                .OrderByDescending(r => r.CreatedAt)
                .FirstOrDefaultAsync();

            if (lastReview == null || lastReview.CreatedAt < DateTime.UtcNow - requirement.TimeWindow)
            {
                context.Succeed(requirement);
            }
            else
            {
                // Add a claim or some indication that the limit was reached
                // Note: Modifying the User principal here is temporary for this request context
                var identity = context.User.Identity as ClaimsIdentity;
                identity?.AddClaim(new Claim("ReviewLimitReached", "true"));
            }
        }
        else
        {
            if (user.ReputationPoints >= requirement.MaxPoints)
            {
                 context.Succeed(requirement);
            }
        }
    }
}