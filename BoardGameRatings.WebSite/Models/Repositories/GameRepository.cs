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
            _context.Games.Add(game);
            _context.SaveChanges();
            return game;
        }

        public void Remove(Game game)
        {
            _context.Games.Remove(game);
            _context.SaveChanges();
        }

        public Game GetById(int id)
        {
            return _context.Games.FirstOrDefault(g => g.Id == id);
        }

        public void Update(Game game)
        {
            _context.Entry(game).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}