using BoardGameRatings.WebSite.Contexts;
using BoardGameRatings.WebSite.ViewModels;
using Moq;

namespace BoardGameRatings.WebSite.Tests.Mocks
{
    public class MockGamesContext : IGamesContext
    {
        private readonly Mock<IGamesContext> _mock;

        public MockGamesContext()
        {
            _mock = new Mock<IGamesContext>();
        }

        public GamesViewModel BuildViewModel()
        {
            return _mock.Object.BuildViewModel();
        }

        public void Remove(int id)
        {
            _mock.Object.Remove(id);
        }

        public MockGamesContext StubBuildViewModelToReturn(GamesViewModel gamesViewModel)
        {
            _mock.Setup(m => m.BuildViewModel())
                .Returns(gamesViewModel);
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