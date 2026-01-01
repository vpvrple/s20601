using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using s20601.Data.Models;
using s20601.Data;

namespace s20601.Services
{
    public class UserService : IUserService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        public UserService(IDbContextFactory<ApplicationDbContext> dbContextFactory, AuthenticationStateProvider authenticationStateProvider)
        {
            _dbContextFactory = dbContextFactory;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<ApplicationUser?> GetUserByUsername(string username)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.Users
                .FirstOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<string?> GetAuthenticatedUserId()
        {
            var auth = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = auth.User;

            return user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
        }
        
        public async Task<ClaimsPrincipal?> GetAuthenticatedUser()
        {
            var auth = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = auth.User;

            return user;
        }
    }
}
