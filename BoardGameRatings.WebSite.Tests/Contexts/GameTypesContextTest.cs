using System.Collections.Generic;
using System.Linq;
using BoardGameRatings.WebSite.Contexts;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.Tests.Mocks;
using BoardGameRatings.WebSite.ViewModels;
using Xunit;

namespace BoardGameRatings.WebSite.Tests.Contexts
{
    public class GameTypesContextTest
    {
        [Fact]
        public void CreatesAGameTypesContext()
        {
            var mockGameTypeRepository = new MockGameTypeRepository();
            var mockGameTypeMapper = new MockGameTypeMapper();
            var gameTypesContext = new GameTypesContext(mockGameTypeRepository, mockGameTypeMapper);

            Assert.NotNull(gameTypesContext);
        }

        [Fact]
        public void ContextBuildsAViewModel()
        {
            var mockGameTypeRepository = new MockGameTypeRepository();
            var mockGameTypeMapper = new MockGameTypeMapper();
            var gameTypesContext = new GameTypesContext(mockGameTypeRepository, mockGameTypeMapper);

            var viewModel = gameTypesContext.BuildViewModel();

            Assert.NotNull(viewModel);
            Assert.IsType<GameTypesViewModel>(viewModel);
        }

        [Fact]
        public void ContextBuildsAViewModelWithAllGameTypes()
        {
            var gameType = new GameType {Description = "GameType 1"};
            var gameTypeViewModel = new GameTypeViewModel {Description = "GameType 1"};
            var gameTypes = new List<GameType> {gameType};
            var mockGameTypeRepository = new MockGameTypeRepository().StubGetAllToReturn(gameTypes);
            var mockGameTypeMapper = new MockGameTypeMapper().StubMapToReturn(gameTypeViewModel);
            var gameTypesContext = new GameTypesContext(mockGameTypeRepository, mockGameTypeMapper);

            var viewModel = gameTypesContext.BuildViewModel();

            Assert.NotNull(viewModel);
            Assert.IsType<GameTypesViewModel>(viewModel);
            Assert.Equal(gameTypes.Count, viewModel.GameTypes.Count());

            var gameTypeViewModels = viewModel.GameTypes.ToList();
            Assert.Equal(gameType.Id, gameTypeViewModels.First().Id);
            Assert.Equal(gameType.Description, gameTypeViewModels.First().Description);

            mockGameTypeRepository.VerifyGetAllCalled();
            mockGameTypeMapper.VerifyMapCalledWith(gameType);
        }

        [Fact]
        public void ContextRemovesAGameType()
        {
            var gameType = new GameType {Description = "GameType 2"};
            var mockGameTypeRepository = new MockGameTypeRepository().StubGetByIdToReturn(gameType);
            var mockGameTypeMapper = new MockGameTypeMapper();
            var gameTypesContext = new GameTypesContext(mockGameTypeRepository, mockGameTypeMapper);

            gameTypesContext.Remove(gameType.Id);

            mockGameTypeRepository.VerifyGetByCalledWith(gameType.Id);
            mockGameTypeRepository.VerifyRemoveCalledWith(gameType);
        }
    }
}