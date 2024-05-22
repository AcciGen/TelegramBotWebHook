namespace DefaultBot.Bot.Models.Entities
{
    public class UserModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public long ChatId { get; set; }
        public string? Username { get; set; }
    }
}
