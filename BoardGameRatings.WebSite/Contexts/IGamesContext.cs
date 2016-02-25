using BoardGameRatings.WebSite.ViewModels;

namespace BoardGameRatings.WebSite.Contexts
{
    public interface IGamesContext
    {
        GamesViewModel BuildViewModel();
    }
}