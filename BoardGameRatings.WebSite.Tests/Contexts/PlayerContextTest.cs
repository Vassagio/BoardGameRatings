using System.Collections.Generic;
using System.Linq;
using BoardGameRatings.WebSite.Contexts;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.Tests.Mocks;
using BoardGameRatings.WebSite.ViewModels;
using Microsoft.AspNet.Mvc.Rendering;
using Xunit;

namespace BoardGameRatings.WebSite.Tests.Contexts
{
    public class PlayerContextTest
    {
        [Fact]
        public void CreatesAPlayerContext()
        {
            var playerContext = BuildPlayerContext();

            Assert.NotNull(playerContext);
        }

        private static PlayerContext BuildPlayerContext(MockPlayerRepository playerRepository = null,
            MockGameRepository gameRepository = null, MockPlayerMapper playerMapper = null,
            MockGameMapper gameMapper = null)
        {
            playerRepository = playerRepository ?? new MockPlayerRepository();
            gameRepository = gameRepository ?? new MockGameRepository();
            playerMapper = playerMapper ?? new MockPlayerMapper();
            gameMapper = gameMapper ?? new MockGameMapper();
            return new PlayerContext(playerRepository, gameRepository, playerMapper, gameMapper);
        }

        [Fact]
        public void ContextBuildsAViewModelWithEditedPlayer()
        {
            var game = new Game {Id = 1, Name = "Game 1"};
            var games = new List<Game> {game};
            var item = new SelectListItem {Value = "1", Text = "Game 1"};
            var gameSelectListItems = new List<SelectListItem> {item};
            var gameViewModel = new GameViewModel {Id = 1, Name = "Game 1"};
            var gamesOwned = new List<GameViewModel> {gameViewModel};
            var player = new Player {Id = 2, FirstName = "First 2", LastName = "Last 2"};
            var playerViewModel = new PlayerViewModel
            {
                Id = 2,
                FirstName = "First 2",
                LastName = "Last 2",
                FullName = "First 2 Last 2",
                Games = gameSelectListItems,
                GamesOwned = gamesOwned
            };

            var mockPlayerRepository =
                new MockPlayerRepository().StubGetAllGamesByToReturn(games).StubGetByToReturn(player);
            var mockGameRepository = new MockGameRepository().StubGetAllToReturn(games);
            var mockPlayerMapper = new MockPlayerMapper().StubMapWithGamesToReturn(playerViewModel);
            var mockGameMapper = new MockGameMapper().StubMapToReturn(gameViewModel).StubSelectMapToReturn(item);
            var playerContext = BuildPlayerContext(mockPlayerRepository, mockGameRepository, mockPlayerMapper,
                mockGameMapper);

            var viewModel = playerContext.BuildViewModel(player.Id);

            Assert.NotNull(viewModel);
            Assert.Equal("First 2 Last 2", viewModel.FullName);
            Assert.Equal(1, viewModel.Games.Count());
            Assert.Equal(1, viewModel.GamesOwned.Count());
            mockPlayerRepository.VerifyGetByCalledWith(player.Id);
            mockPlayerRepository.VerifyGetAllGamesByCalledWith(player.Id);
            mockGameRepository.VerifyGetAllCalled();
            mockPlayerMapper.VerifyMapCalledWith(player, gameSelectListItems, gamesOwned);
            mockGameMapper.VerifySelectMapCalledWith(game);
            mockGameMapper.VerifyMapCalledWith(game);
        }


        [Fact]
        public void ContextBuildsAViewModelWithNewPlayer()
        {
            var game = new Game {Id = 1, Name = "Game 1"};
            var games = new List<Game> {game};
            var item = new SelectListItem {Value = "1", Text = "Game 1"};
            var gameSelectListItems = new List<SelectListItem> {item};
            var gamesOwned = new List<GameViewModel>();
            var player = new Player();
            var playerViewModel = new PlayerViewModel
            {
                Games = gameSelectListItems,
                GamesOwned = gamesOwned,
                FullName = string.Empty
            };
            var mockPlayerRepository = new MockPlayerRepository().StubGetByToReturn(player);
            var mockGameRepository = new MockGameRepository().StubGetAllToReturn(games);
            var mockPlayerMapper = new MockPlayerMapper().StubMapWithGamesToReturn(playerViewModel);
            var mockGameMapper = new MockGameMapper().StubSelectMapToReturn(item);
            var playerContext = BuildPlayerContext(mockPlayerRepository, mockGameRepository, mockPlayerMapper,
                mockGameMapper);

            var viewModel = playerContext.BuildViewModel();

            Assert.NotNull(viewModel);
            Assert.Equal(string.Empty, viewModel.FullName);
            Assert.Equal(1, viewModel.Games.Count());
            mockPlayerRepository.VerifyGetByCalledWith(player.Id);
            mockGameRepository.VerifyGetAllCalled();
            mockPlayerMapper.VerifyMapCalledWith(player, gameSelectListItems, gamesOwned);
            mockGameMapper.VerifySelectMapCalledWith(game);
        }

        [Fact]
        public void ContextSavesAnUpdatedPlayer()
        {
            var player = new Player {Id = 2, FirstName = "First 2", LastName = "Last 2"};
            var playerViewModel = new PlayerViewModel
            {
                Id = 2,
                FirstName = "Updated First",
                LastName = "Updated Last"
            };
            var mockPlayerRepository = new MockPlayerRepository().StubGetByToReturn(player);
            var playerContext = BuildPlayerContext(mockPlayerRepository);

            playerContext.Save(playerViewModel);

            mockPlayerRepository.VerifyGetByCalledWith(playerViewModel.Id);
            mockPlayerRepository.VerifyUpdateCalledWith(player);
        }

        [Fact]
        public void ContextSavesANewPlayer()
        {
            var player = new Player {Id = 4, FirstName = "New First", LastName = "New Last"};
            var playerViewModel = new PlayerViewModel {Id = 4, FirstName = "New First", LastName = "New Last"};
            var mockPlayerRepository = new MockPlayerRepository();
            var mockPlayerMapper = new MockPlayerMapper().StubMapToReturn(player);
            var playerContext = BuildPlayerContext(mockPlayerRepository, playerMapper: mockPlayerMapper);

            playerContext.Save(playerViewModel);

            mockPlayerRepository.VerifyGetByCalledWith(playerViewModel.Id);
            mockPlayerRepository.VerifyAddCalledWith(player);
            mockPlayerMapper.VerifyMapCalledWith(playerViewModel);
        }

        [Fact]
        public void ContextAddsAnOwnedGame()
        {
            var gameId = 1;
            var playerId = 1;
            var mockPlayerRepository = new MockPlayerRepository();
            var playerContext = BuildPlayerContext(mockPlayerRepository);

            playerContext.AddGameOwned(playerId, gameId);

            mockPlayerRepository.VerifyAddGameOwnedCalledWith(playerId, gameId);
        }

        [Fact]
        public void ContextRemovesAnOwnedGame()
        {
            var gameId = 1;
            var playerId = 1;
            var mockPlayerRepository = new MockPlayerRepository();
            var playerContext = BuildPlayerContext(mockPlayerRepository);

            playerContext.RemoveGameOwned(playerId, gameId);

            mockPlayerRepository.VerifyRemoveGameOwnedCalledWith(playerId, gameId);
        }
    }
}