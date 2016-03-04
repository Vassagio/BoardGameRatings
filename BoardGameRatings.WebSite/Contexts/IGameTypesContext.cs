using BoardGameRatings.WebSite.ViewModels;

namespace BoardGameRatings.WebSite.Contexts
{
    public interface IGameTypesContext
    {
        GameTypesViewModel BuildViewModel();
        void Remove(int id);
    }
}