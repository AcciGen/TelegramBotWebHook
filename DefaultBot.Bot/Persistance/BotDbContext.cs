using DefaultBot.Bot.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DefaultBot.Bot.Persistance
{
    public class BotDbContext : DbContext
    {
        public BotDbContext(DbContextOptions<BotDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<UserModel> Users { get; set; }
    }
}
