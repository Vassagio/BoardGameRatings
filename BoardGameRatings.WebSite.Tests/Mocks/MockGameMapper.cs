using BoardGameRatings.WebSite.Mappers;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.ViewModels;
using Moq;

namespace BoardGameRatings.WebSite.Tests.Mocks
{
    public class MockGameMapper : IGameMapper
    {
        private readonly Mock<IGameMapper> _mock;

        public MockGameMapper()
        {
            _mock = new Mock<IGameMapper>();
        }

        public Game Map(GameViewModel viewModel)
        {
            return _mock.Object.Map(viewModel);
        }

        public GameViewModel Map(Game game)
        {
            return _mock.Object.Map(game);
        }

        public void VerifyMapCalledWith(Game game)
        {
            _mock.Verify(m => m.Map(game));
        }

        public void VerifyMapCalledWith(GameViewModel gameViewModel)
        {
            _mock.Verify(m => m.Map(gameViewModel));
        }

        public MockGameMapper StubMapToReturn(GameViewModel gameViewModel)
        {
            _mock.Setup(m => m.Map(It.IsAny<Game>())).Returns(gameViewModel);
            return this;
        }

        public MockGameMapper StubMapToReturn(Game game)
        {
            _mock.Setup(m => m.Map(It.IsAny<GameViewModel>())).Returns(game);
            return this;
        }
    }
}