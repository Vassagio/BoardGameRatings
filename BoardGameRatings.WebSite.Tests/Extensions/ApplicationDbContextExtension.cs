using System.Collections.Generic;
using BoardGameRatings.WebSite.Models;

namespace BoardGameRatings.WebSite.Tests.Extensions
{
    internal static class ApplicationDbContextExtension
    {
        public static ApplicationDbContext GamesContain(this ApplicationDbContext context, IEnumerable<Game> games)
        {
            context.Games.AddRange(games);
            context.SaveChanges();
            return context;
        }
    }
}