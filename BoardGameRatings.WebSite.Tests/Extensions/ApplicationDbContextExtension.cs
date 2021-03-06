﻿using System.Collections.Generic;
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

        public static ApplicationDbContext CategoriesContain(this ApplicationDbContext context,
            IEnumerable<Category> categories)
        {
            context.Categories.AddRange(categories);
            context.SaveChanges();
            return context;
        }

        public static ApplicationDbContext PlayersContain(this ApplicationDbContext context, IEnumerable<Player> players)
        {
            context.Players.AddRange(players);
            context.SaveChanges();
            return context;
        }

        public static ApplicationDbContext PlayerGamesContain(this ApplicationDbContext context,
            IEnumerable<PlayerGame> playerGames)
        {
            context.PlayerGames.AddRange(playerGames);
            context.SaveChanges();
            return context;
        }


        public static ApplicationDbContext GameCategoriesContain(this ApplicationDbContext context,
            IEnumerable<GameCategory> gameCategories)
        {
            context.GameCategories.AddRange(gameCategories);
            context.SaveChanges();
            return context;
        }

        public static ApplicationDbContext GamePlayedDatesContain(this ApplicationDbContext context,
            IEnumerable<GamePlayedDate> gamePlayedDates)
        {
            context.GamePlayedDates.AddRange(gamePlayedDates);
            context.SaveChanges();
            return context;
        }
    }
}