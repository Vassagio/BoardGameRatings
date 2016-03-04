using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.ViewModels;
using Microsoft.AspNet.Mvc.Rendering;

namespace BoardGameRatings.WebSite.Mappers
{
    public interface ICategoryMapper
    {
        Category Map(CategoryViewModel viewModel);
        CategoryViewModel Map(Category category);
        SelectListItem SelectMap(Category category);
    }
}