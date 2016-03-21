using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;

namespace BoardGameRatings.WebSite.Models.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext _context;

        public GameRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Game> GetAll()
        {
            return _context.Games;
        }

        public Game Add(Game player)
        {
            var found = GetBy(player.Name);
            if (found != null)
                return found;

            _context.Games.Add(player);
            _context.SaveChanges();
            return player;
        }

        public void Remove(Game game)
        {
            _context.Games.Remove(game);
            _context.SaveChanges();
        }

        public Game GetBy(int gameId)
        {
            return _context.Games.FirstOrDefault(g => g.Id == gameId);
        }

        public Game GetBy(string name)
        {
            return _context.Games.FirstOrDefault(g => g.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
        }

        public void Update(Game game)
        {
            _context.Entry(game).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IEnumerable<Category> GetAllCategoriesBy(int gameId)
        {
            return _context.GameCategories
                .Where(pg => pg.GameId == gameId)
                .Select(pg => pg.Category);
        }

        public GameCategory GetGameCategoryBy(int gameId, int categoryId)
        {
            return _context.GameCategories
                .FirstOrDefault(pg => pg.GameId == gameId && pg.CategoryId == categoryId);
        }

        public void AddElectedCategory(int gameId, int categoryId)
        {
            if (GetBy(gameId) == null)
                throw new ArgumentException("Game does not exist");

            if (!IsValidCategory(categoryId))
                throw new ArgumentException("Category does not exist");

            if (GetGameCategoryBy(gameId, categoryId) != null)
                throw new ArgumentException("This category has already been elected for this game.");

            var gameCategory = new GameCategory {CategoryId = categoryId, GameId = gameId};
            _context.GameCategories.Add(gameCategory);
            _context.SaveChanges();
        }

        public void RemoveElectedCategory(int gameId, int categoryId)
        {
            var gameCategory = GetGameCategoryBy(gameId, categoryId);
            if (gameCategory == null)
                return;

            _context.GameCategories.Remove(gameCategory);
            _context.SaveChanges();
        }

        public IEnumerable<DateTime> GetAllPlayedDatesBy(int gameId)
        {
            return _context.GamePlayedDates
                .Where(pg => pg.GameId == gameId)
                .Select(pg => pg.PlayedDate);
        }

        public void AddPlayedDate(int gameId, DateTime playedDate)
        {
            if (GetBy(gameId) == null)
                throw new ArgumentException("Game does not exist");

            if (GetGamePlayedDateBy(gameId, playedDate) != null)
                throw new ArgumentException("This date has already been set for this game.");

            var gamePlayedDate = new GamePlayedDate {GameId = gameId, PlayedDate = playedDate};
            _context.GamePlayedDates.Add(gamePlayedDate);
            _context.SaveChanges();
        }

        public GamePlayedDate GetGamePlayedDateBy(int gameId, DateTime playedDate)
        {
            return _context.GamePlayedDates
                .FirstOrDefault(pg => pg.GameId == gameId && pg.PlayedDate == playedDate);
        }

        public void RemovePlayedGame(int gameId, DateTime playedGame)
        {
            var gamePlayedDate = GetGamePlayedDateBy(gameId, playedGame);
            if (gamePlayedDate == null)
                return;

            _context.GamePlayedDates.Remove(gamePlayedDate);
            _context.SaveChanges();
        }

        private bool IsValidCategory(int categoryId)
        {
            return _context.Categories.Any(c => c.Id == categoryId);
        }
    }
}