using System.Collections.Generic;
using BoardGameRatings.WebSite.Mappers;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.ViewModels;
using Microsoft.AspNet.Mvc.Rendering;
using Xunit;
using System.Linq;

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
        public void MapPlayerToPlayerViewModelWithGames() {
            var player = new Player {
                Id = 2,
                FirstName = "First Name",
                LastName = "Last Name"
            };
            var games = new List<SelectListItem> {new SelectListItem() {Value = "1", Text = "Game 1"} };
            var mapper = new PlayerMapper();
            var viewModel = mapper.Map(player, games);

            Assert.Equal(player.Id, viewModel.Id);
            Assert.Equal(player.FirstName, viewModel.FirstName);
            Assert.Equal(player.LastName, viewModel.LastName);
            Assert.Equal(1, viewModel.Games.Count());
        }

        [Fact]
        public void MapPlayerToPlayerViewModelWithGamesOnly()
        {
            var player = new Player();            
            var games = new List<SelectListItem> { new SelectListItem() { Value = "1", Text = "Game 1" } };
            var mapper = new PlayerMapper();
            var viewModel = mapper.Map(player, games);

            Assert.NotNull(viewModel);
            Assert.Equal(1, viewModel.Games.Count());
        }

        [Fact]
        public void MapNullToPlayerViewModelWithGamesOnly() {            
            var games = new List<SelectListItem> { new SelectListItem() { Value = "1", Text = "Game 1" } };
            var mapper = new PlayerMapper();
            var viewModel = mapper.Map(null, games);

            Assert.NotNull(viewModel);
            Assert.Equal(1, viewModel.Games.Count());
        }
    }
}