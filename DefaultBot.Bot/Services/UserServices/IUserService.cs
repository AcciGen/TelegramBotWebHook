using DefaultBot.Bot.Models.Entities;

namespace DefaultBot.Bot.Services.UserServices
{
    public interface IUserService
    {
        Task CreateUser(UserModel user);
        Task<UserModel> GetUser(long ChatId);
        Task<IEnumerable<UserModel>> GetAllUsers();
    }
}
