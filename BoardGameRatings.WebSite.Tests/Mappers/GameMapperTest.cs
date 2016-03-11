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

        [Fact]
        public void MapGameToGameViewModelWithCategoriesAndElectedCategories()
        {
            var game = new Game
            {
                Id = 2,
                Name = "Game 1"
            };
            var categories = new List<SelectListItem> {new SelectListItem {Value = "1", Text = "Category 1"}};
            var electedCategories = new List<CategoryViewModel>
            {
                new CategoryViewModel {Id = 1, Description = "Category 1"}
            };
            var mapper = new GameMapper();
            var viewModel = mapper.Map(game, categories, electedCategories);

            Assert.Equal(game.Id, viewModel.Id);
            Assert.Equal(game.Name, viewModel.Name);
            Assert.Equal(1, viewModel.Categories.Count());
            Assert.Equal(1, viewModel.ElectedCategories.Count());
        }

        [Fact]
        public void MapGameToGameViewModelWithCategoriesAndElectedCategoriesOnly()
        {
            var game = new Game();
            var categories = new List<SelectListItem> {new SelectListItem {Value = "1", Text = "Category 1"}};
            var electedCategories = new List<CategoryViewModel>
            {
                new CategoryViewModel {Id = 1, Description = "Category 1"}
            };
            var mapper = new GameMapper();
            var viewModel = mapper.Map(game, categories, electedCategories);

            Assert.NotNull(viewModel);
            Assert.Equal(1, viewModel.Categories.Count());
            Assert.Equal(1, viewModel.ElectedCategories.Count());
        }

        [Fact]
        public void MapNullToGameViewModelWithCategpriesAndEmptyElectedCategoriesOnly()
        {
            var categories = new List<SelectListItem> {new SelectListItem {Value = "1", Text = "Category 1"}};
            var mapper = new GameMapper();
            var viewModel = mapper.Map(null, categories, new List<CategoryViewModel>());

            Assert.NotNull(viewModel);
            Assert.Equal(1, viewModel.Categories.Count());
            Assert.Equal(0, viewModel.ElectedCategories.Count());
        }
    }
}