using BoardGameRatings.WebSite.Contexts;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.Tests.Mocks;
using BoardGameRatings.WebSite.ViewModels;
using Xunit;

namespace BoardGameRatings.WebSite.Tests.Contexts
{
    public class GameContextTest
    {
        [Fact]
        public void CreatesAGameContext()
        {
            var mockGameRepository = new MockGameRepository();
            var mockGameMapper = new MockGameMapper();
            var gameContext = new GameContext(mockGameRepository, mockGameMapper);

            Assert.NotNull(gameContext);
        }

        [Fact]
        public void ContextBuildsAViewModelWithEditedGame()
        {
            var game = new Game {Id = 2, Name = "Game 2", Description = "This is game 2"};
            var gameViewModel = new GameViewModel {Id = 2, Name = "Game 2", Description = "This is game 2"};
            var mockGameRepository = new MockGameRepository().StubGetByIdToReturn(game);
            var mockGameMapper = new MockGameMapper().StubMapToReturn(gameViewModel);
            var gameContext = new GameContext(mockGameRepository, mockGameMapper);

            var viewModel = gameContext.BuildViewModel(game.Id);

            Assert.NotNull(viewModel);
            mockGameRepository.VerifyGetByIdCalledWith(game.Id);
            mockGameMapper.VerifyMapCalledWith(game);
        }


        [Fact]
        public void ContextBuildsAViewModelWithNewGame()
        {
            var mockGameRepository = new MockGameRepository();
            var mockGameMapper = new MockGameMapper();
            var gameContext = new GameContext(mockGameRepository, mockGameMapper);

            var viewModel = gameContext.BuildViewModel();

            Assert.NotNull(viewModel);
        }

        [Fact]
        public void ContextSavesAnUpdatedGame()
        {
            var game = new Game {Id = 2, Name = "Game 2", Description = "This is game 2"};
            var gameViewModel = new GameViewModel
            {
                Id = 2,
                Name = "Updated Game",
                Description = "This is an updated game"
            };
            var mockGameRepository = new MockGameRepository().StubGetByIdToReturn(game);
            var mockGameMapper = new MockGameMapper();
            var gameContext = new GameContext(mockGameRepository, mockGameMapper);

            gameContext.Save(gameViewModel);

            mockGameRepository.VerifyGetByIdCalledWith(gameViewModel.Id);
            mockGameRepository.VerifyUpdateCalledWith(game);
        }

        [Fact]
        public void ContextSavesANewGame()
        {
            var game = new Game {Id = 4, Name = "New Game", Description = "This is new game"};
            var gameViewModel = new GameViewModel {Id = 4, Name = "New Game", Description = "This is a new game"};
            var mockGameRepository = new MockGameRepository();
            var mockGameMapper = new MockGameMapper().StubMapToReturn(game);
            var gameContext = new GameContext(mockGameRepository, mockGameMapper);

            gameContext.Save(gameViewModel);

            mockGameRepository.VerifyGetByIdCalledWith(gameViewModel.Id);
            mockGameRepository.VerifyAddCalledWith(game);
            mockGameMapper.VerifyMapCalledWith(gameViewModel);
        }
    }
}