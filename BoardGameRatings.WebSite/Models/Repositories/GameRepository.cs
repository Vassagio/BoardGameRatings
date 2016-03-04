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

        public Game Add(Game game)
        {
            var found = GetBy(game.Name);
            if (found != null)
                return found;

            _context.Games.Add(game);
            _context.SaveChanges();
            return game;
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

        public Game GetBy(string name) {
            return _context.Games.FirstOrDefault(g => g.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
        }

        public void Update(Game game)
        {
            _context.Entry(game).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}