using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.ViewModels;
using Microsoft.AspNet.Mvc.Rendering;

namespace BoardGameRatings.WebSite.Mappers
{
    public class CategoryMapper : ICategoryMapper
    {
        public Category Map(CategoryViewModel viewModel)
        {
            return new Category
            {
                Id = viewModel.Id,
                Description = viewModel.Description
            };
        }

        public CategoryViewModel Map(Category category)
        {
            return new CategoryViewModel
            {
                Id = category.Id,
                Description = category.Description
            };
        }

        public SelectListItem SelectMap(Category category)
        {
            return new SelectListItem
            {
                Value = category.Id.ToString(),
                Text = category.Description
            };
        }
    }
}