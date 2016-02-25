using System.Collections.Generic;

namespace BoardGameRatings.WebSite.Models.Repositories
{
    public interface IGameRepository
    {
        IEnumerable<Game> GetAll();
        Game Add(Game game);
        void Remove(Game game);
        Game GetById(int id);
        void Update(Game game);
    }
}