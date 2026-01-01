using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using s20601.Data;

namespace s20601.Components.Account.Policies;

public class MinimumPointsHandler : AuthorizationHandler<MinimumPointsRequirement>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public MinimumPointsHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, MinimumPointsRequirement requirement)
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            return;
        }

        using var dbContext = await _dbContextFactory.CreateDbContextAsync();
        var user = await dbContext.Users.FindAsync(userId);

        if (user != null && user.ReputationPoints >= requirement.MinimumPoints)
        {
            context.Succeed(requirement);
        }
    }
}