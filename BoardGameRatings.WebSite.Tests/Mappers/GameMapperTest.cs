using System.Collections.Generic;
using System.Linq;
using BoardGameRatings.WebSite.Mappers;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.ViewModels;
using Microsoft.AspNet.Mvc.Rendering;
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

        [Fact]
        public void MapGameToSelectListItem()
        {
            var game = new Game
            {
                Id = 2,
                Name = "Game Name",
                Description = "This is a game"
            };
            var mapper = new GameMapper();
            var item = mapper.SelectMap(game);

            Assert.Equal(game.Id.ToString(), item.Value);
            Assert.Equal(game.Name, item.Text);
        }

        [Theory]
        [InlineData(false, false, false)]
        [InlineData(true, false, false)]
        [InlineData(false, true, false)]
        [InlineData(false, false, true)]
        [InlineData(false, true, true)]
        [InlineData(true, false, true)]
        [InlineData(true, true, false)]
        [InlineData(true, true, true)]
        public void MapGameToGameViewModel(bool hasCategories, bool hasElectedCategories, bool hasPlayedDates)
        {
            var game = new Game
            {
                Id = 2,
                Name = "Game 1"
            };
            var categories = hasCategories ? new List<SelectListItem> {new SelectListItem {Value = "1", Text = "Category 1"}} : null;
            var electedCategories = hasElectedCategories ? new List<CategoryViewModel> {new CategoryViewModel {Id = 1, Description = "Category 1"}} : null;
            var playedDates = hasPlayedDates ? new List<PlayedDateViewModel> {new PlayedDateViewModel {Id = 1, FormattedPlayedDate = "1/1/2016"}} : null;
            var mapper = new GameMapper();
            var viewModel = mapper.Map(game, categories, electedCategories, playedDates);


            Assert.NotNull(viewModel);
            Assert.Equal(hasCategories ? 1 : 0, viewModel.Categories.Count());
            Assert.Equal(hasElectedCategories ? 1 : 0, viewModel.ElectedCategories.Count());
            Assert.Equal(hasPlayedDates ? 1 : 0, viewModel.PlayedDates.Count());
        }

        [Theory]
        [InlineData(false, false, false)]
        [InlineData(true, false, false)]
        [InlineData(false, true, false)]
        [InlineData(false, false, true)]
        [InlineData(false, true, true)]
        [InlineData(true, false, true)]
        [InlineData(true, true, false)]
        public void MapNullToGameViewModel(bool hasCategories, bool hasElectedCategories, bool hasPlayedDates)
        {
            var categories = hasCategories ? new List<SelectListItem> {new SelectListItem {Value = "1", Text = "Category 1"}} : null;
            var electedCategories = hasElectedCategories ? new List<CategoryViewModel> {new CategoryViewModel {Id = 1, Description = "Category 1"}} : null;
            var playedDates = hasPlayedDates ? new List<PlayedDateViewModel> {new PlayedDateViewModel {Id = 1, FormattedPlayedDate = "1/1/2016"}} : null;
            var mapper = new GameMapper();
            var viewModel = mapper.Map(null, categories, electedCategories, playedDates);


            Assert.NotNull(viewModel);
        }
    }
}