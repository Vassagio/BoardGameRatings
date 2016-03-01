using BoardGameRatings.WebSite.Mappers;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.ViewModels;
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
        public void MapPlayerToPlayerViewModel()
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
    }
}