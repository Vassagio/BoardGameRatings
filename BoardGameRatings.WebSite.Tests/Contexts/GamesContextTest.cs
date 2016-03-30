using System.Collections.Generic;
using System.Linq;
using BoardGameRatings.WebSite.Contexts;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.Tests.Mocks;
using BoardGameRatings.WebSite.ViewModels;
using Xunit;

namespace BoardGameRatings.WebSite.Tests.Contexts
{
    public class GamesContextTest
    {
        [Fact]
        public void CreatesAGamesContext()
        {
            var mockGameRepository = new MockGameRepository();
            var mockGameMapper = new MockGameMapper();
            var gamesContext = new GamesContext(mockGameRepository, mockGameMapper);

            Assert.NotNull(gamesContext);
        }

        [Fact]
        public void ContextBuildsAViewModel()
        {
            var mockGameRepository = new MockGameRepository();
            var mockGameMapper = new MockGameMapper();
            var gamesContext = new GamesContext(mockGameRepository, mockGameMapper);

            var viewModel = gamesContext.BuildViewModel();

            Assert.NotNull(viewModel);
            Assert.IsType<GamesViewModel>(viewModel);
        }

        [Fact]
        public void ContextBuildsAViewModelWithAllGames()
        {
            var game = new Game {Name = "Game 1"};
            var gameViewModel = new GameViewModel {Name = "Game 1"};
            var games = new List<Game> {game};
            var mockGameRepository = new MockGameRepository().StubGetAllToReturn(games);
            var mockGameMapper = new MockGameMapper().StubMapToReturn(gameViewModel);
            var gamesContext = new GamesContext(mockGameRepository, mockGameMapper);

            var viewModel = gamesContext.BuildViewModel();

            Assert.NotNull(viewModel);
            Assert.IsType<GamesViewModel>(viewModel);
            Assert.Equal(games.Count, viewModel.Games.Count());

            var gameViewModels = viewModel.Games.ToList();
            Assert.Equal(game.Id, gameViewModels.First()
                .Id);
            Assert.Equal(game.Name, gameViewModels.First()
                .Name);
            Assert.Equal(game.Description, gameViewModels.First()
                .Description);

            mockGameRepository.VerifyGetAllCalled();
            mockGameMapper.VerifyMapCalledWith(game);
        }

        [Fact]
        public void ContextRemovesAGame()
        {
            var game = new Game {Name = "Game 2"};
            var mockGameRepository = new MockGameRepository().StubGetByToReturn(game);
            var mockGameMapper = new MockGameMapper();
            var gamesContext = new GamesContext(mockGameRepository, mockGameMapper);

            gamesContext.Remove(game.Id);

            mockGameRepository.VerifyGetByCalledWith(game.Id);
            mockGameRepository.VerifyRemoveCalledWith(game);
        }
    }
}