using BoardGameRatings.WebSite.ViewModels;

namespace BoardGameRatings.WebSite.Contexts
{
    public interface IGameTypeContext
    {
        GameTypeViewModel BuildViewModel(int? id = null);
        void Save(GameTypeViewModel model);
    }
}