using System.Collections.Generic;
using System.Linq;
using BoardGameRatings.WebSite.Mappers;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.ViewModels;
using Microsoft.AspNet.Mvc.Rendering;
using Xunit;

namespace BoardGameRatings.WebSite.Tests.Mappers
{
    public class PlayerMapperTest
    {
        [Fact]
        public void MapPlayerViewModelToPlayer()
        {
            var viewModel = new PlayerViewModel
            {
                Id = 2,
                FirstName = "First Name",
                LastName = "Last Name"
            };
            var mapper = new PlayerMapper();
            var player = mapper.Map(viewModel);

            Assert.Equal(viewModel.Id, player.Id);
            Assert.Equal(viewModel.FirstName, player.FirstName);
            Assert.Equal(viewModel.LastName, player.LastName);
        }

        [Fact]
        public void MapExistingPlayerToPlayerViewModel()
        {
            var player = new Player
            {
                Id = 2,
                FirstName = "First Name",
                LastName = "Last Name"
            };
            var mapper = new PlayerMapper();
            var viewModel = mapper.Map(player);

            Assert.Equal(player.Id, viewModel.Id);
            Assert.Equal(player.FirstName, viewModel.FirstName);
            Assert.Equal(player.LastName, viewModel.LastName);
            Assert.Equal("First Name Last Name", viewModel.FullName);
        }

        [Fact]
        public void MapNewPlayerToPlayerViewModel()
        {
            var player = new Player();
            var mapper = new PlayerMapper();
            var viewModel = mapper.Map(player);

            Assert.NotNull(viewModel);
        }

        [Fact]
        public void MapPlayerToPlayerViewModelWithGamesAndGamesOwned()
        {
            var player = new Player
            {
                Id = 2,
                FirstName = "First Name",
                LastName = "Last Name"
            };
            var games = new List<SelectListItem> {new SelectListItem {Value = "1", Text = "Game 1"}};
            var gamesOwned = new List<GameViewModel> {new GameViewModel {Id = 1, Name = "Game 1"}};
            var mapper = new PlayerMapper();
            var viewModel = mapper.Map(player, games, gamesOwned);

            Assert.Equal(player.Id, viewModel.Id);
            Assert.Equal(player.FirstName, viewModel.FirstName);
            Assert.Equal(player.LastName, viewModel.LastName);
            Assert.Equal("First Name Last Name", viewModel.FullName);
            Assert.Equal(1, viewModel.Games.Count());
            Assert.Equal(1, viewModel.GamesOwned.Count());
        }

        [Fact]
        public void MapPlayerToPlayerViewModelWithGamesAndGamesOwnedOnly()
        {
            var player = new Player();
            var games = new List<SelectListItem> {new SelectListItem {Value = "1", Text = "Game 1"}};
            var gamesOwned = new List<GameViewModel> {new GameViewModel {Id = 1, Name = "Game 1"}};
            var mapper = new PlayerMapper();
            var viewModel = mapper.Map(player, games, gamesOwned);

            Assert.NotNull(viewModel);
            Assert.Equal(1, viewModel.Games.Count());
        }

        [Fact]
        public void MapNullToPlayerViewModelWithGamesAndEmptyGamesOwnedOnly()
        {
            var games = new List<SelectListItem> {new SelectListItem {Value = "1", Text = "Game 1"}};
            var mapper = new PlayerMapper();
            var viewModel = mapper.Map(null, games, new List<GameViewModel>());

            Assert.NotNull(viewModel);
            Assert.Equal(1, viewModel.Games.Count());
        }
    }
}