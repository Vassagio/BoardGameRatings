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
        public void CreatesAGameContext()
        {
            var gameRepository = new MockGameRepository();
            var gamesContext = new GamesContext(gameRepository);

            Assert.NotNull(gamesContext);
        }

        [Fact]
        public void ContextBuildsAViewModel()
        {
            var gameRepository = new MockGameRepository();
            var gamesContext = new GamesContext(gameRepository);

            var viewModel = gamesContext.BuildViewModel();

            Assert.NotNull(viewModel);
            Assert.IsType<GamesViewModel>(viewModel);
        }

        [Fact]
        public void ContextBuildsAViewModelWithAllGames()
        {
            var games = new List<Game>
            {
                new Game {Name = "Game 1"},
                new Game {Name = "Game 2"},
                new Game {Name = "Game 3"}
            };
            var mockGameRepository = new MockGameRepository().StubGetAllToReturn(games);
            var gamesContext = new GamesContext(mockGameRepository);

            var viewModel = gamesContext.BuildViewModel();

            Assert.NotNull(viewModel);
            Assert.IsType<GamesViewModel>(viewModel);
            Assert.Equal(games.Count, viewModel.Games.Count());

            var gameViewModels = viewModel.Games.ToList();
            for (var i = 0; i < games.Count; i++)
            {
                Assert.Equal(games[i].Id, gameViewModels[i].Id);
                Assert.Equal(games[i].Name, gameViewModels[i].Name);
                Assert.Equal(games[i].Description, gameViewModels[i].Description);
            }
            mockGameRepository.VerifyGetAllCalled();
        }

        [Fact]
        public void ContextRemovesAGame()
        {
            var game = new Game {Name = "Game 2"};
            var mockGameRepository = new MockGameRepository().StubGetByIdToReturn(game);
            var gamesContext = new GamesContext(mockGameRepository);

            gamesContext.Remove(game.Id);

            mockGameRepository.VerifyGetByIdCalledWith(game.Id);
            mockGameRepository.VerifyRemoveCalledWith(game);
        }
    }
}