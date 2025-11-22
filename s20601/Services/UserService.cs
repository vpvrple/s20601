using Microsoft.EntityFrameworkCore;
using s20601.Data.Models;
using s20601.Data;

namespace s20601.Services
{
    public class UserService : IUserService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        public UserService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<ApplicationUser?> GetUserByUsernameAsync(string username)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.Users
                .FirstOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(string id)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.Users
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
