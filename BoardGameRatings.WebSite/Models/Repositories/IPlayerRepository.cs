using System.Collections.Generic;

namespace BoardGameRatings.WebSite.Models.Repositories
{
    public interface IPlayerRepository : IRepository<Player>
    {
        IEnumerable<Game> GetAllGamesBy(int playerId);
    }
}