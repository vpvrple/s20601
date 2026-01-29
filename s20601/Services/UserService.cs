using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using s20601.Data.Models;
using s20601.Data;
using s20601.Events.Commands;
using s20601.Events.Commands.UserAvatars;
using s20601.Events.Queries;
using s20601.Services.External.Azure;

namespace s20601.Services
{
    public class UserService : IUserService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly IMediator _mediator;
        public UserService(IDbContextFactory<ApplicationDbContext> dbContextFactory, AuthenticationStateProvider authenticationStateProvider, IMediator mediator)
        {
            _dbContextFactory = dbContextFactory;
            _authenticationStateProvider = authenticationStateProvider;
            _mediator = mediator;
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

        public async Task<string?> GetUserAvatarByUserId(string userId)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();

            var user = await context.Users
                .Where(x => userId == x.Id)
                .FirstOrDefaultAsync();
            
            if (user.Avatar == null)
            {
                return null;
            }
            return await _mediator.Send(new GetAzureUserAvatarQuery(AzureBlobType.UserAvatars, user.Avatar));
        }

        public async Task<string?> UpdateUserProfileDescription(string? profileDescription)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            var auth = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = auth.User;

            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
            
            var userFromDb = await context.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();

            if (userFromDb == null)
            {
                return null;
            }
            
            userFromDb.ProfileDescription = profileDescription;

            await context.SaveChangesAsync();
            
            return userFromDb.ProfileDescription;
        }

        public async Task<string?> UpdateUserAvatar(Stream? fileStream, string? avatar)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            var auth = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = auth.User;

            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";

            var userFromDb = await context.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
            
            if (userFromDb == null)
            {
                return null;
            }
            
            if (userFromDb.Avatar != null)
            {
                await _mediator.Send(new UserAvatarUpdatedCommand(AzureBlobType.UserAvatars, userFromDb.Avatar));
            }

            if (avatar is null)
            {
                userFromDb.Avatar = null;
            }
            else
            {
                if (fileStream == null)
                {
                    throw new ArgumentNullException(nameof(fileStream), "File stream cannot be null if avatar is provided.");
                }
                var fileExtension = Path.GetExtension(avatar).ToLowerInvariant();
                var newFileName = $"{Guid.NewGuid()}{fileExtension}";

                userFromDb.Avatar = newFileName;
                await _mediator.Send(new UploadUserAvatarCommand(AzureBlobType.UserAvatars, fileStream, newFileName));
            }

            await context.SaveChangesAsync();

            return userFromDb.Avatar;
        }
    }
}
