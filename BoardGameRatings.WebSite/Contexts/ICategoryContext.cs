using BoardGameRatings.WebSite.ViewModels;

namespace BoardGameRatings.WebSite.Contexts
{
    public interface ICategoryContext
    {
        CategoryViewModel BuildViewModel(int? id = null);
        void Save(CategoryViewModel model);
    }
}