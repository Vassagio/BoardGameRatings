using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.ViewModels;

namespace BoardGameRatings.WebSite.Mappers
{
    public interface ICategoryMapper : IMapper<Category, CategoryViewModel>, ISelectListItemMapper<Category>
    {
    }
}