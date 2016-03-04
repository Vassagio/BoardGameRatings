using BoardGameRatings.WebSite.Contexts;
using BoardGameRatings.WebSite.ViewModels;
using Moq;

namespace BoardGameRatings.WebSite.Tests.Mocks
{
    public class MockPlayersContext : IPlayersContext
    {
        private readonly Mock<IPlayersContext> _mock;

        public MockPlayersContext()
        {
            _mock = new Mock<IPlayersContext>();
        }

        public PlayersViewModel BuildViewModel()
        {
            return _mock.Object.BuildViewModel();
        }

        public void Remove(int id)
        {
            _mock.Object.Remove(id);
        }

        public MockPlayersContext StubBuildViewModelToReturn(PlayersViewModel playersViewModel)
        {
            _mock.Setup(m => m.BuildViewModel()).Returns(playersViewModel);
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