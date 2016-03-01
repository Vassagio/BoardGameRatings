using System.Collections.Generic;

namespace BoardGameRatings.WebSite.Models.Repositories
{
    public interface IPlayerRepository
    {
        IEnumerable<Player> GetAll();
        Player Add(Player player);
        void Remove(Player player);
        Player GetById(int id);
        void Update(Player player);
    }
}