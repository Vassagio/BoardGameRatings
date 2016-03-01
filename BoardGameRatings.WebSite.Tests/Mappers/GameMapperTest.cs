using BoardGameRatings.WebSite.Mappers;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.ViewModels;
using Xunit;

namespace BoardGameRatings.WebSite.Tests.Mappers
{
    public class GameMapperTest
    {
        [Fact]
        public void MapGameViewModelToGame()
        {
            var viewModel = new GameViewModel
            {
                Id = 2,
                Name = "Game Name",
                Description = "This is a game"
            };
            var mapper = new GameMapper();
            var game = mapper.Map(viewModel);

            Assert.Equal(viewModel.Id, game.Id);
            Assert.Equal(viewModel.Name, game.Name);
            Assert.Equal(viewModel.Description, game.Description);
        }

        [Fact]
        public void MapGameToGameViewModel()
        {
            var game = new Game
            {
                Id = 2,
                Name = "Game Name",
                Description = "This is a game"
            };
            var mapper = new GameMapper();
            var viewModel = mapper.Map(game);

            Assert.Equal(game.Id, viewModel.Id);
            Assert.Equal(game.Name, viewModel.Name);
            Assert.Equal(game.Description, viewModel.Description);
        }
    }
}