using MediatR;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using s20601.Data;
using s20601.Data.Models;
using s20601.Events.Commands.UserAvatars;
using s20601.Events.Commands.UserLogIns;
using s20601.Events.Queries;
using s20601.Services.External.Azure;
using System.Security.Claims;

namespace s20601.Services
{
    public class UserService : IUserService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly IMediator _mediator;
        public UserService(IDbContextFactory<ApplicationDbContext> dbContextFactory, IMediator mediator, AuthenticationStateProvider authenticationStateProvider)
        {
            _dbContextFactory = dbContextFactory;
            _mediator = mediator;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<ApplicationUser?> GetUserById(string id)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.Users
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<string?> GetUserIdByUsername(string username)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.Users
                .Where(x => x.UserName == username)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();
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

        public async Task DailyLogin()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is { IsAuthenticated: true })
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId != null)
                {
                    using var context = await _dbContextFactory.CreateDbContextAsync();
                    var appUser = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

                    if (appUser != null)
                    {
                        if (appUser.LastDailyLogin == null || appUser.LastDailyLogin < DateTime.UtcNow.Date)
                        {
                            appUser.LastDailyLogin = DateTime.UtcNow;
                            context.Users.Update(appUser);
                            var result = await context.SaveChangesAsync();
                            if (result > 0)
                            {
                                await _mediator.Publish(new UserLoggedInCommand(appUser.Id, 1));
                            }
                        }
                    }
                }
            }
        }
    }
}