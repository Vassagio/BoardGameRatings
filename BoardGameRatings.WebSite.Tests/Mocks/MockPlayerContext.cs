using BoardGameRatings.WebSite.Contexts;
using BoardGameRatings.WebSite.ViewModels;
using Moq;

namespace BoardGameRatings.WebSite.Tests.Mocks
{
    public class MockPlayerContext : IPlayerContext
    {
        private readonly Mock<IPlayerContext> _mock;

        public MockPlayerContext()
        {
            _mock = new Mock<IPlayerContext>();
        }

        public PlayerViewModel BuildViewModel(int? id = null)
        {
            return _mock.Object.BuildViewModel(id);
        }

        public void Save(PlayerViewModel model)
        {
            _mock.Object.Save(model);
        }

        public void AddGameOwned(int playerId, int gameId)
        {
            _mock.Object.AddGameOwned(playerId, gameId);
        }

        public MockPlayerContext StubBuildViewModelToReturn(PlayerViewModel playerViewModel)
        {
            _mock.Setup(m => m.BuildViewModel(It.IsAny<int?>())).Returns(playerViewModel);
            return this;
        }

        public void VerifyBuildViewModelCalledWith(int? id = null)
        {
            _mock.Verify(m => m.BuildViewModel(id));
        }

        public void VerifySaveCalledWith(PlayerViewModel playerViewModel)
        {
            _mock.Verify(m => m.Save(playerViewModel));
        }

        public void VerifyAddGameOwnedCalledWith(int playerId, int gameId)
        {
            _mock.Verify(m => m.AddGameOwned(playerId, gameId));
        }
    }
}