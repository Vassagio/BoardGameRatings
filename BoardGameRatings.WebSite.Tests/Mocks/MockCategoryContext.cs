using BoardGameRatings.WebSite.Contexts;
using BoardGameRatings.WebSite.ViewModels;
using Moq;

namespace BoardGameRatings.WebSite.Tests.Mocks
{
    public class MockCategoryContext : ICategoryContext
    {
        private readonly Mock<ICategoryContext> _mock;

        public MockCategoryContext()
        {
            _mock = new Mock<ICategoryContext>();
        }

        public CategoryViewModel BuildViewModel(int? id = null)
        {
            return _mock.Object.BuildViewModel(id);
        }

        public void Save(CategoryViewModel model)
        {
            _mock.Object.Save(model);
        }

        public MockCategoryContext StubBuildViewModelToReturn(CategoryViewModel categoryViewModel)
        {
            _mock.Setup(m => m.BuildViewModel(It.IsAny<int?>())).Returns(categoryViewModel);
            return this;
        }

        public void VerifyBuildViewModelCalledWith(int? id = null)
        {
            _mock.Verify(m => m.BuildViewModel(id));
        }

        public void VerifySaveCalledWith(CategoryViewModel categoryViewModel)
        {
            _mock.Verify(m => m.Save(categoryViewModel));
        }
    }
}