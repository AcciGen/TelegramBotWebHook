using DefaultBot.Bot.Models.Entities;

namespace DefaultBot.Bot.Services.UserRepositories
{
    public interface IUserRepository
    {
        public Task Add(UserModel user);
        public Task<List<UserModel>> GetAllUsers();
    }
}
