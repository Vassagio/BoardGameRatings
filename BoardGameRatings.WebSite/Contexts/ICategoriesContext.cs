using BoardGameRatings.WebSite.ViewModels;

namespace BoardGameRatings.WebSite.Contexts
{
    public interface ICategoriesContext
    {
        CategoriesViewModel BuildViewModel();
        void Remove(int id);
    }
}