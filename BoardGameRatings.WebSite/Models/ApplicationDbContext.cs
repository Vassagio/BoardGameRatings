using System;
using BoardGameRatings.WebSite.Models.ModelBuilders;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

namespace BoardGameRatings.WebSite.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(IServiceProvider serviceProvider, DbContextOptions options)
            : base(serviceProvider, options)
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerGame> PlayerGames { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<GameCategory> GameCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            new GameModelBuilder(builder.Entity<Game>()).Build();
            new PlayerModelBuilder(builder.Entity<Player>()).Build();
            new PlayerGameModelBuilder(builder.Entity<PlayerGame>()).Build();
            new CategoryModelBuilder(builder.Entity<Category>()).Build();
            new GameCategoryModelBuilder(builder.Entity<GameCategory>()).Build();
        }
    }
}