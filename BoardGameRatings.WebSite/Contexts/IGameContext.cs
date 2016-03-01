using BoardGameRatings.WebSite.ViewModels;

namespace BoardGameRatings.WebSite.Contexts
{
    public interface IGameContext
    {
        GameViewModel BuildViewModel(int? id);
        void Save(GameViewModel model);
    }
}