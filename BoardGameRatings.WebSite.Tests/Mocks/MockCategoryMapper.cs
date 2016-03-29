using BoardGameRatings.WebSite.Mappers;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.ViewModels;
using Microsoft.AspNet.Mvc.Rendering;
using Moq;

namespace BoardGameRatings.WebSite.Tests.Mocks
{
    public class MockCategoryMapper : ICategoryMapper
    {
        private readonly Mock<ICategoryMapper> _mock;

        public MockCategoryMapper()
        {
            _mock = new Mock<ICategoryMapper>();
        }

        public Category Map(CategoryViewModel viewModel)
        {
            return _mock.Object.Map(viewModel);
        }

        public CategoryViewModel Map(Category category)
        {
            return _mock.Object.Map(category);
        }

        public SelectListItem SelectMap(Category category)
        {
            return _mock.Object.SelectMap(category);
        }

        public MockCategoryMapper StubMapToReturn(CategoryViewModel categoryViewModel)
        {
            _mock.Setup(m => m.Map(It.IsAny<Category>()))
                .Returns(categoryViewModel);
            return this;
        }

        public void VerifyMapCalledWith(Category category)
        {
            _mock.Verify(m => m.Map(category));
        }

        public MockCategoryMapper StubMapToReturn(Category category)
        {
            _mock.Setup(m => m.Map(It.IsAny<CategoryViewModel>()))
                .Returns(category);
            return this;
        }


        public void VerifyMapCalledWith(CategoryViewModel categoryViewModel)
        {
            _mock.Verify(m => m.Map(categoryViewModel));
        }

        public MockCategoryMapper StubSelectMapToReturn(SelectListItem item)
        {
            _mock.Setup(m => m.SelectMap(It.IsAny<Category>()))
                .Returns(item);
            return this;
        }

        public void VerifySelectMapCalledWith(Category category)
        {
            _mock.Verify(m => m.SelectMap(category));
        }
    }
}