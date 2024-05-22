using DefaultBot.Bot.Models.Entities;

namespace DefaultBot.Bot.Services.UserServices
{
    public class UserService : IUserService
    {
        public Task CreateUser(UserModel user)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserModel>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> GetUser(long ChatId)
        {
            throw new NotImplementedException();
        }
    }
}
