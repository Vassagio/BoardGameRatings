using BoardGameRatings.WebSite.Mappers;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.ViewModels;
using Microsoft.AspNet.Mvc.Rendering;
using Moq;

namespace BoardGameRatings.WebSite.Tests.Mocks
{
    public class MockGameTypeMapper : IGameTypeMapper
    {
        private readonly Mock<IGameTypeMapper> _mock;

        public MockGameTypeMapper()
        {
            _mock = new Mock<IGameTypeMapper>();
        }

        public GameType Map(GameTypeViewModel viewModel)
        {
            return _mock.Object.Map(viewModel);
        }

        public GameTypeViewModel Map(GameType gameType)
        {
            return _mock.Object.Map(gameType);
        }

        public SelectListItem SelectMap(GameType gameType)
        {
            return _mock.Object.SelectMap(gameType);
        }

        public MockGameTypeMapper StubMapToReturn(GameTypeViewModel gameTypeViewModel)
        {
            _mock.Setup(m => m.Map(It.IsAny<GameType>())).Returns(gameTypeViewModel);
            return this;
        }

        public void VerifyMapCalledWith(GameType gameType)
        {
            _mock.Verify(m => m.Map(gameType));
        }

        public MockGameTypeMapper StubMapToReturn(GameType gameType)
        {
            _mock.Setup(m => m.Map(It.IsAny<GameTypeViewModel>())).Returns(gameType);
            return this;
        }


        public void VerifyMapCalledWith(GameTypeViewModel gameTypeViewModel)
        {
            _mock.Verify(m => m.Map(gameTypeViewModel));
        }
    }
}