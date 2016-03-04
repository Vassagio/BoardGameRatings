using BoardGameRatings.WebSite.Contexts;
using BoardGameRatings.WebSite.ViewModels;
using Moq;

namespace BoardGameRatings.WebSite.Tests.Mocks
{
    public class MockGameTypesContext : IGameTypesContext
    {
        private readonly Mock<IGameTypesContext> _mock;

        public MockGameTypesContext()
        {
            _mock = new Mock<IGameTypesContext>();
        }

        public GameTypesViewModel BuildViewModel()
        {
            return _mock.Object.BuildViewModel();
        }

        public void Remove(int id)
        {
            _mock.Object.Remove(id);
        }

        public MockGameTypesContext StubBuildViewModelToReturn(GameTypesViewModel gameTypesViewModel)
        {
            _mock.Setup(m => m.BuildViewModel()).Returns(gameTypesViewModel);
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