using BoardGameRatings.WebSite.ViewModels;

namespace BoardGameRatings.WebSite.Contexts
{
    public interface IPlayerContext
    {
        PlayerViewModel BuildViewModel(int? id);
        void Save(PlayerViewModel model);
        void AddGameOwned(int playerId, int gameId);
        void RemoveGameOwned(int playerId, int gameId);
    }
}