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

        public async Task<ApplicationUser?> GetUserByNicknameAsync(string nickname)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.Users
                .FirstOrDefaultAsync(x => x.Nickname == nickname);
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(string id)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.Users
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
