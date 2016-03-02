using System.Collections.Generic;
using BoardGameRatings.WebSite.Contexts;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.Tests.Mocks;
using BoardGameRatings.WebSite.ViewModels;
using Xunit;
using System.Linq;
using Microsoft.AspNet.Mvc.Rendering;

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

        private static PlayerContext BuildPlayerContext(MockPlayerRepository playerRepository = null, MockGameRepository gameRepository = null, MockPlayerMapper playerMapper = null, MockGameMapper gameMapper = null)
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
            var games = new List<Game>{game};
            var item = new SelectListItem {Value = "1", Text = "Game 1"};
            var gameSelectListItems = new List<SelectListItem> {item};
            var player = new Player {Id = 2, FirstName = "First 2", LastName = "Last 2"};
            var playerViewModel = new PlayerViewModel {Id = 2, FirstName = "First 2", LastName = "Last 2", Games = gameSelectListItems};
            var mockPlayerRepository = new MockPlayerRepository().StubGetByToReturn(player);
            var mockGameRepository = new MockGameRepository().StubGetAllToReturn(games);
            var mockPlayerMapper = new MockPlayerMapper().StubMapWithGamesToReturn(playerViewModel);
            var mockGameMapper = new MockGameMapper().StubSelectMapToReturn(item);
            var playerContext = BuildPlayerContext(mockPlayerRepository, mockGameRepository, mockPlayerMapper, mockGameMapper);

            var viewModel = playerContext.BuildViewModel(player.Id);

            Assert.NotNull(viewModel);
            Assert.Equal("First 2 Last 2", viewModel.GetFullName());
            Assert.Equal(1, viewModel.Games.Count());
            mockPlayerRepository.VerifyGetByCalledWith(player.Id);
            mockGameRepository.VerifyGetAllCalled();
            mockPlayerMapper.VerifyMapCalledWith(player, gameSelectListItems);
            mockGameMapper.VerifySelectMapCalledWith(game);
        }


        [Fact]
        public void ContextBuildsAViewModelWithNewPlayer()
        {
            var game = new Game { Id = 1, Name = "Game 1" };
            var games = new List<Game> { game };
            var item = new SelectListItem { Value = "1", Text = "Game 1" };
            var gameSelectListItems = new List<SelectListItem> { item };
            var player = new Player();
            var playerViewModel = new PlayerViewModel { Games = gameSelectListItems };
            var mockPlayerRepository = new MockPlayerRepository().StubGetByToReturn(player);
            var mockGameRepository = new MockGameRepository().StubGetAllToReturn(games);
            var mockPlayerMapper = new MockPlayerMapper().StubMapWithGamesToReturn(playerViewModel);
            var mockGameMapper = new MockGameMapper().StubSelectMapToReturn(item);
            var playerContext = BuildPlayerContext(mockPlayerRepository, mockGameRepository, mockPlayerMapper, mockGameMapper);

            var viewModel = playerContext.BuildViewModel();

            Assert.NotNull(viewModel);
            Assert.Equal(string.Empty, viewModel.GetFullName());
            Assert.Equal(1, viewModel.Games.Count());
            mockPlayerRepository.VerifyGetByCalledWith(player.Id);
            mockGameRepository.VerifyGetAllCalled();
            mockPlayerMapper.VerifyMapCalledWith(player, gameSelectListItems);
            mockGameMapper.VerifySelectMapCalledWith(game);
        }

        [Fact]
        public void ContextSavesAnUpdatedPlayer()
        {
            var player = new Player {Id = 2, FirstName = "First 2", LastName = "Last 2"};
            var playerViewModel = new PlayerViewModel{Id = 2,FirstName = "Updated First",                LastName = "Updated Last"
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
    }
}