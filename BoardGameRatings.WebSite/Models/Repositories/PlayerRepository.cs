using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;

namespace BoardGameRatings.WebSite.Models.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly ApplicationDbContext _context;

        public PlayerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Player> GetAll()
        {
            return _context.Players;
        }

        public IEnumerable<Game> GetAllGamesBy(int playerId)
        {
            return _context.PlayerGames
                .Where(pg => pg.PlayerId == playerId)
                .Select(pg => pg.Game);
        }

        public Player Add(Player player)
        {
            _context.Players.Add(player);
            _context.SaveChanges();
            return player;
        }

        public void Remove(Player player)
        {
            _context.Players.Remove(player);
            _context.SaveChanges();
        }

        public Player GetBy(int playerId)
        {
            return _context.Players.FirstOrDefault(g => g.Id == playerId);
        }

        public void Update(Player player)
        {
            _context.Entry(player).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}