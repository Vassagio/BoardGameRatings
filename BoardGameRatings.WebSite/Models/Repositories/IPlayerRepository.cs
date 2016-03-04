using System.Collections.Generic;

namespace BoardGameRatings.WebSite.Models.Repositories
{
    public interface IPlayerRepository : IRepository<Player>
    {
        IEnumerable<Game> GetAllGamesBy(int playerId);
        void AddGameOwned(int playerId, int gameId);
        PlayerGame GetPlayerGameBy(int playerId, int gameId);
    }
}