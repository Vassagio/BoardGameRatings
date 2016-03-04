using BoardGameRatings.WebSite.Contexts;
using BoardGameRatings.WebSite.ViewModels;
using Moq;

namespace BoardGameRatings.WebSite.Tests.Mocks
{
    public class MockGameTypeContext : IGameTypeContext
    {
        private readonly Mock<IGameTypeContext> _mock;

        public MockGameTypeContext()
        {
            _mock = new Mock<IGameTypeContext>();
        }

        public GameTypeViewModel BuildViewModel(int? id = null)
        {
            return _mock.Object.BuildViewModel(id);
        }

        public void Save(GameTypeViewModel model)
        {
            _mock.Object.Save(model);
        }

        public MockGameTypeContext StubBuildViewModelToReturn(GameTypeViewModel gameTypeViewModel)
        {
            _mock.Setup(m => m.BuildViewModel(It.IsAny<int?>())).Returns(gameTypeViewModel);
            return this;
        }

        public void VerifyBuildViewModelCalledWith(int? id = null)
        {
            _mock.Verify(m => m.BuildViewModel(id));
        }

        public void VerifySaveCalledWith(GameTypeViewModel gameTypeViewModel)
        {
            _mock.Verify(m => m.Save(gameTypeViewModel));
        }
    }
}