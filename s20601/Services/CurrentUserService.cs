using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace s20601.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        public CurrentUserService(AuthenticationStateProvider authenticationStateProvider)
        {
            _authenticationStateProvider = authenticationStateProvider;
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
