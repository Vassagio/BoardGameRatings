using BoardGameRatings.WebSite.Mappers;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.ViewModels;
using Xunit;

namespace BoardGameRatings.WebSite.Tests.Mappers
{
    public class CategoryMapperTest
    {
        [Fact]
        public void MapCategoryViewModelToCategory()
        {
            var viewModel = new CategoryViewModel
            {
                Id = 2,
                Description = "Category Description"
            };
            var mapper = new CategoryMapper();
            var category = mapper.Map(viewModel);

            Assert.Equal(viewModel.Id, category.Id);
            Assert.Equal(viewModel.Description, category.Description);
        }

        [Fact]
        public void MapCategoryToCategoryViewModel()
        {
            var category = new Category
            {
                Id = 2,
                Description = "Category Description"
            };
            var mapper = new CategoryMapper();
            var viewModel = mapper.Map(category);

            Assert.Equal(category.Id, viewModel.Id);
            Assert.Equal(category.Description, viewModel.Description);
        }

        [Fact]
        public void MapCategoryToSelectListItem()
        {
            var category = new Category
            {
                Id = 2,
                Description = "Category Description"
            };
            var mapper = new CategoryMapper();
            var item = mapper.SelectMap(category);

            Assert.Equal(category.Id.ToString(), item.Value);
            Assert.Equal(category.Description, item.Text);
        }
    }
}