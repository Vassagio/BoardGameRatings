using System.Collections.Generic;
using BoardGameRatings.WebSite.Mappers;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.ViewModels;
using Microsoft.AspNet.Mvc.Rendering;
using Moq;

namespace BoardGameRatings.WebSite.Tests.Mocks
{
    public class MockPlayerMapper : IPlayerMapper
    {
        private readonly Mock<IPlayerMapper> _mock;

        public MockPlayerMapper()
        {
            _mock = new Mock<IPlayerMapper>();
        }

        public Player Map(PlayerViewModel viewModel)
        {
            return _mock.Object.Map(viewModel);
        }

        public PlayerViewModel Map(Player player)
        {
            return _mock.Object.Map(player);
        }

        public PlayerViewModel Map(Player player, IEnumerable<SelectListItem> games,
            IEnumerable<GameViewModel> gamesOwned)
        {
            return _mock.Object.Map(player, games, gamesOwned);
        }


        public void VerifyMapCalledWith(Player player)
        {
            _mock.Verify(m => m.Map(player));
        }

        public void VerifyMapCalledWith(Player player, IEnumerable<SelectListItem> games,
            IEnumerable<GameViewModel> gamesOwned)
        {
            _mock.Verify(m => m.Map(player, games, gamesOwned));
        }

        public void VerifyMapCalledWith(PlayerViewModel playerViewModel)
        {
            _mock.Verify(m => m.Map(playerViewModel));
        }

        public MockPlayerMapper StubMapToReturn(PlayerViewModel playerViewModel)
        {
            _mock.Setup(m => m.Map(It.IsAny<Player>())).Returns(playerViewModel);
            return this;
        }

        public MockPlayerMapper StubMapWithGamesToReturn(PlayerViewModel playerViewModel)
        {
            _mock.Setup(
                m =>
                    m.Map(It.IsAny<Player>(), It.IsAny<IEnumerable<SelectListItem>>(),
                        It.IsAny<IEnumerable<GameViewModel>>())).Returns(playerViewModel);
            return this;
        }

        public MockPlayerMapper StubMapToReturn(Player player)
        {
            _mock.Setup(m => m.Map(It.IsAny<PlayerViewModel>())).Returns(player);
            return this;
        }
    }
}