using BoardGameRatings.WebSite.Mappers;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.ViewModels;
using Xunit;

namespace BoardGameRatings.WebSite.Tests.Mappers
{
    public class GameTypeMapperTest
    {
        [Fact]
        public void MapGameTypeViewModelToGameType()
        {
            var viewModel = new GameTypeViewModel
            {
                Id = 2,
                Description = "GameType Description"
            };
            var mapper = new GameTypeMapper();
            var gameType = mapper.Map(viewModel);

            Assert.Equal(viewModel.Id, gameType.Id);
            Assert.Equal(viewModel.Description, gameType.Description);
        }

        [Fact]
        public void MapGameTypeToGameTypeViewModel()
        {
            var gameType = new GameType
            {
                Id = 2,
                Description = "GameType Description"
            };
            var mapper = new GameTypeMapper();
            var viewModel = mapper.Map(gameType);

            Assert.Equal(gameType.Id, viewModel.Id);
            Assert.Equal(gameType.Description, viewModel.Description);
        }

        [Fact]
        public void MapGameTypeToSelectListItem()
        {
            var gameType = new GameType
            {
                Id = 2,
                Description = "GameType Description"
            };
            var mapper = new GameTypeMapper();
            var item = mapper.SelectMap(gameType);

            Assert.Equal(gameType.Id.ToString(), item.Value);
            Assert.Equal(gameType.Description, item.Text);
        }
    }
}