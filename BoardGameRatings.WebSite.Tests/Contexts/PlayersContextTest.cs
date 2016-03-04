using System.Collections.Generic;
using System.Linq;
using BoardGameRatings.WebSite.Contexts;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.Tests.Mocks;
using BoardGameRatings.WebSite.ViewModels;
using Xunit;

namespace BoardGameRatings.WebSite.Tests.Contexts
{
    public class PlayersContextTest
    {
        [Fact]
        public void CreatesAPlayersContext()
        {
            var mockPlayerRepository = new MockPlayerRepository();
            var mockPlayerMapper = new MockPlayerMapper();
            var playersContext = new PlayersContext(mockPlayerRepository, mockPlayerMapper);

            Assert.NotNull(playersContext);
        }

        [Fact]
        public void ContextBuildsAViewModel()
        {
            var mockPlayerRepository = new MockPlayerRepository();
            var mockPlayerMapper = new MockPlayerMapper();
            var playersContext = new PlayersContext(mockPlayerRepository, mockPlayerMapper);

            var viewModel = playersContext.BuildViewModel();

            Assert.NotNull(viewModel);
            Assert.IsType<PlayersViewModel>(viewModel);
        }

        [Fact]
        public void ContextBuildsAViewModelWithAllPlayers()
        {
            var player = new Player {Id = 1, FirstName = "First 1", LastName = "Last 1"};
            var playerViewModel = new PlayerViewModel {Id = 1, FirstName = "First 1", LastName = "Last 1"};
            var players = new List<Player> {player};
            var mockPlayerRepository = new MockPlayerRepository().StubGetAllToReturn(players);
            var mockPlayerMapper = new MockPlayerMapper().StubMapToReturn(playerViewModel);
            var playersContext = new PlayersContext(mockPlayerRepository, mockPlayerMapper);

            var viewModel = playersContext.BuildViewModel();

            Assert.NotNull(viewModel);
            Assert.IsType<PlayersViewModel>(viewModel);
            Assert.Equal(players.Count, viewModel.Players.Count());

            var playerViewModels = viewModel.Players.ToList();
            for (var i = 0; i < players.Count; i++)
            {
                Assert.Equal(players[i].Id, playerViewModels[i].Id);
                Assert.Equal(players[i].FirstName, playerViewModels[i].FirstName);
                Assert.Equal(players[i].LastName, playerViewModels[i].LastName);
            }
            mockPlayerRepository.VerifyGetAllCalled();
            mockPlayerMapper.VerifyMapCalledWith(player);
        }

        [Fact]
        public void ContextRemovesAPlayer()
        {
            var player = new Player {Id = 1, FirstName = "First 2", LastName = "Last 2"};
            var mockPlayerRepository = new MockPlayerRepository().StubGetByToReturn(player);
            var mockPlayerMapper = new MockPlayerMapper();
            var playersContext = new PlayersContext(mockPlayerRepository, mockPlayerMapper);

            playersContext.Remove(player.Id);

            mockPlayerRepository.VerifyGetByCalledWith(player.Id);
            mockPlayerRepository.VerifyRemoveCalledWith(player);
        }
    }
}