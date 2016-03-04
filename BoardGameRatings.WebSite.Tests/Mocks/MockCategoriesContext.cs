using BoardGameRatings.WebSite.Contexts;
using BoardGameRatings.WebSite.ViewModels;
using Moq;

namespace BoardGameRatings.WebSite.Tests.Mocks
{
    public class MockCategoriesContext : ICategoriesContext
    {
        private readonly Mock<ICategoriesContext> _mock;

        public MockCategoriesContext()
        {
            _mock = new Mock<ICategoriesContext>();
        }

        public CategoriesViewModel BuildViewModel()
        {
            return _mock.Object.BuildViewModel();
        }

        public void Remove(int id)
        {
            _mock.Object.Remove(id);
        }

        public MockCategoriesContext StubBuildViewModelToReturn(CategoriesViewModel categoriesViewModel)
        {
            _mock.Setup(m => m.BuildViewModel()).Returns(categoriesViewModel);
            return this;
        }

        public void VerifyBuildViewModelCalled()
        {
            _mock.Verify(m => m.BuildViewModel());
        }

        public void VerifyRemoveCalledWith(int id)
        {
            _mock.Verify(m => m.Remove(id));
        }
    }
}