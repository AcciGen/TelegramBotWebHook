using DefaultBot.Bot.Models.Entities;
using DefaultBot.Bot.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DefaultBot.Bot.Services.UserRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BotDbContext _appBotDbContext;
        public UserRepository(BotDbContext context)
        {
            _appBotDbContext = context;
        }

        public async Task Add(UserModel user)
        {
            await _appBotDbContext.Users.AddAsync(user);
            await _appBotDbContext.SaveChangesAsync();
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            return await _appBotDbContext.Users.ToListAsync();
        }
    }
}
