using System;
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

        public PlayerGame GetPlayerGameBy(int playerId, int gameId)
        {
            return _context.PlayerGames
                .FirstOrDefault(pg => pg.PlayerId == playerId && pg.GameId == gameId);
        }

        public Player Add(Player player)
        {
            var found = GetBy(player.FirstName, player.LastName);
            if (found != null)
                return found;
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

        public void AddGameOwned(int playerId, int gameId)
        {
            if (GetPlayerGameBy(playerId, gameId) != null)
                return;

            var playerGame = new PlayerGame {GameId = gameId, PlayerId = playerId};
            _context.PlayerGames.Add(playerGame);
            _context.SaveChanges();
        }

        public Player GetBy(string firstName, string lastName)
        {
            return _context.Players.FirstOrDefault(g =>
                g.FirstName.Equals(firstName, StringComparison.CurrentCultureIgnoreCase) &&
                g.LastName.Equals(lastName, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}