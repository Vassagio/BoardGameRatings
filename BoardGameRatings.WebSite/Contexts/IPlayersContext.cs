using BoardGameRatings.WebSite.ViewModels;

namespace BoardGameRatings.WebSite.Contexts
{
    public interface IPlayersContext
    {
        PlayersViewModel BuildViewModel();
        void Remove(int id);
    }
}