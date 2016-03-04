using BoardGameRatings.WebSite.Contexts;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.Tests.Mocks;
using BoardGameRatings.WebSite.ViewModels;
using Xunit;

namespace BoardGameRatings.WebSite.Tests.Contexts
{
    public class CategoryContextTest
    {
        [Fact]
        public void CreatesACategoryContext()
        {
            var mockCategoryRepository = new MockCategoryRepository();
            var mockCategoryMapper = new MockCategoryMapper();
            var categoryContext = new CategoryContext(mockCategoryRepository, mockCategoryMapper);

            Assert.NotNull(categoryContext);
        }

        [Fact]
        public void ContextBuildsAViewModelWithEditedCategory()
        {
            var category = new Category {Id = 2, Description = "This is category 2"};
            var categoryViewModel = new CategoryViewModel {Id = 2, Description = "This is category 2"};
            var mockCategoryRepository = new MockCategoryRepository().StubGetByIdToReturn(category);
            var mockCategoryMapper = new MockCategoryMapper().StubMapToReturn(categoryViewModel);
            var categoryContext = new CategoryContext(mockCategoryRepository, mockCategoryMapper);

            var viewModel = categoryContext.BuildViewModel(category.Id);

            Assert.NotNull(viewModel);
            mockCategoryRepository.VerifyGetByCalledWith(category.Id);
            mockCategoryMapper.VerifyMapCalledWith(category);
        }


        [Fact]
        public void ContextBuildsAViewModelWithNewCategory()
        {
            var mockCategoryRepository = new MockCategoryRepository();
            var mockCategoryMapper = new MockCategoryMapper();
            var categoryContext = new CategoryContext(mockCategoryRepository, mockCategoryMapper);

            var viewModel = categoryContext.BuildViewModel();

            Assert.NotNull(viewModel);
        }

        [Fact]
        public void ContextSavesAnUpdatedCategory()
        {
            var category = new Category {Id = 2, Description = "This is category 2"};
            var categoryViewModel = new CategoryViewModel
            {
                Id = 2,
                Description = "This is an updated category"
            };
            var mockCategoryRepository = new MockCategoryRepository().StubGetByIdToReturn(category);
            var mockCategoryMapper = new MockCategoryMapper();
            var categoryContext = new CategoryContext(mockCategoryRepository, mockCategoryMapper);

            categoryContext.Save(categoryViewModel);

            mockCategoryRepository.VerifyGetByCalledWith(categoryViewModel.Id);
            mockCategoryRepository.VerifyUpdateCalledWith(category);
        }

        [Fact]
        public void ContextSavesANewCategory()
        {
            var category = new Category {Id = 4, Description = "This is new category"};
            var categoryViewModel = new CategoryViewModel {Id = 4, Description = "This is a new category"};
            var mockCategoryRepository = new MockCategoryRepository();
            var mockCategoryMapper = new MockCategoryMapper().StubMapToReturn(category);
            var categoryContext = new CategoryContext(mockCategoryRepository, mockCategoryMapper);

            categoryContext.Save(categoryViewModel);

            mockCategoryRepository.VerifyGetByCalledWith(categoryViewModel.Id);
            mockCategoryRepository.VerifyAddCalledWith(category);
            mockCategoryMapper.VerifyMapCalledWith(categoryViewModel);
        }
    }
}