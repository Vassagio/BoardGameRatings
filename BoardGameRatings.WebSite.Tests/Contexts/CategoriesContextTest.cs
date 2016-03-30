using System.Collections.Generic;
using System.Linq;
using BoardGameRatings.WebSite.Contexts;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.Tests.Mocks;
using BoardGameRatings.WebSite.ViewModels;
using Xunit;

namespace BoardGameRatings.WebSite.Tests.Contexts
{
    public class CategoriesContextTest
    {
        [Fact]
        public void CreatesACategoriesContext()
        {
            var mockCategoryRepository = new MockCategoryRepository();
            var mockCategoryMapper = new MockCategoryMapper();
            var categoriesContext = new CategoriesContext(mockCategoryRepository, mockCategoryMapper);

            Assert.NotNull(categoriesContext);
        }

        [Fact]
        public void ContextBuildsAViewModel()
        {
            var mockCategoryRepository = new MockCategoryRepository();
            var mockCategoryMapper = new MockCategoryMapper();
            var categoriesContext = new CategoriesContext(mockCategoryRepository, mockCategoryMapper);

            var viewModel = categoriesContext.BuildViewModel();

            Assert.NotNull(viewModel);
            Assert.IsType<CategoriesViewModel>(viewModel);
        }

        [Fact]
        public void ContextBuildsAViewModelWithAllCategories()
        {
            var category = new Category {Description = "Category 1"};
            var categoryViewModel = new CategoryViewModel {Description = "Category 1"};
            var categories = new List<Category> {category};
            var mockCategoryRepository = new MockCategoryRepository().StubGetAllToReturn(categories);
            var mockCategoryMapper = new MockCategoryMapper().StubMapToReturn(categoryViewModel);
            var categoriesContext = new CategoriesContext(mockCategoryRepository, mockCategoryMapper);

            var viewModel = categoriesContext.BuildViewModel();

            Assert.NotNull(viewModel);
            Assert.IsType<CategoriesViewModel>(viewModel);
            Assert.Equal(categories.Count, viewModel.Categories.Count());

            var categoryViewModels = viewModel.Categories.ToList();
            Assert.Equal(category.Id, categoryViewModels.First()
                .Id);
            Assert.Equal(category.Description, categoryViewModels.First()
                .Description);

            mockCategoryRepository.VerifyGetAllCalled();
            mockCategoryMapper.VerifyMapCalledWith(category);
        }

        [Fact]
        public void ContextRemovesACategory()
        {
            var category = new Category {Description = "Category 2"};
            var mockCategoryRepository = new MockCategoryRepository().StubGetByIdToReturn(category);
            var mockCategoryMapper = new MockCategoryMapper();
            var categoriesContext = new CategoriesContext(mockCategoryRepository, mockCategoryMapper);

            categoriesContext.Remove(category.Id);

            mockCategoryRepository.VerifyGetByCalledWith(category.Id);
            mockCategoryRepository.VerifyRemoveCalledWith(category);
        }
    }
}