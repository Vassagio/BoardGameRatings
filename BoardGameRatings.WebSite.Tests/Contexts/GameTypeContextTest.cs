using BoardGameRatings.WebSite.Contexts;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.Tests.Mocks;
using BoardGameRatings.WebSite.ViewModels;
using Xunit;

namespace BoardGameRatings.WebSite.Tests.Contexts
{
    public class GameTypeContextTest
    {
        [Fact]
        public void CreatesAGameTypeContext()
        {
            var mockGameTypeRepository = new MockGameTypeRepository();
            var mockGameTypeMapper = new MockGameTypeMapper();
            var gameTypeContext = new GameTypeContext(mockGameTypeRepository, mockGameTypeMapper);

            Assert.NotNull(gameTypeContext);
        }

        [Fact]
        public void ContextBuildsAViewModelWithEditedGameType()
        {
            var gameType = new GameType {Id = 2, Description = "This is gameType 2"};
            var gameTypeViewModel = new GameTypeViewModel {Id = 2, Description = "This is gameType 2"};
            var mockGameTypeRepository = new MockGameTypeRepository().StubGetByIdToReturn(gameType);
            var mockGameTypeMapper = new MockGameTypeMapper().StubMapToReturn(gameTypeViewModel);
            var gameTypeContext = new GameTypeContext(mockGameTypeRepository, mockGameTypeMapper);

            var viewModel = gameTypeContext.BuildViewModel(gameType.Id);

            Assert.NotNull(viewModel);
            mockGameTypeRepository.VerifyGetByCalledWith(gameType.Id);
            mockGameTypeMapper.VerifyMapCalledWith(gameType);
        }


        [Fact]
        public void ContextBuildsAViewModelWithNewGameType()
        {
            var mockGameTypeRepository = new MockGameTypeRepository();
            var mockGameTypeMapper = new MockGameTypeMapper();
            var gameTypeContext = new GameTypeContext(mockGameTypeRepository, mockGameTypeMapper);

            var viewModel = gameTypeContext.BuildViewModel();

            Assert.NotNull(viewModel);
        }

        [Fact]
        public void ContextSavesAnUpdatedGameType()
        {
            var gameType = new GameType {Id = 2, Description = "This is gameType 2"};
            var gameTypeViewModel = new GameTypeViewModel
            {
                Id = 2,
                Description = "This is an updated gameType"
            };
            var mockGameTypeRepository = new MockGameTypeRepository().StubGetByIdToReturn(gameType);
            var mockGameTypeMapper = new MockGameTypeMapper();
            var gameTypeContext = new GameTypeContext(mockGameTypeRepository, mockGameTypeMapper);

            gameTypeContext.Save(gameTypeViewModel);

            mockGameTypeRepository.VerifyGetByCalledWith(gameTypeViewModel.Id);
            mockGameTypeRepository.VerifyUpdateCalledWith(gameType);
        }

        [Fact]
        public void ContextSavesANewGameType()
        {
            var gameType = new GameType {Id = 4, Description = "This is new gameType"};
            var gameTypeViewModel = new GameTypeViewModel {Id = 4, Description = "This is a new gameType"};
            var mockGameTypeRepository = new MockGameTypeRepository();
            var mockGameTypeMapper = new MockGameTypeMapper().StubMapToReturn(gameType);
            var gameTypeContext = new GameTypeContext(mockGameTypeRepository, mockGameTypeMapper);

            gameTypeContext.Save(gameTypeViewModel);

            mockGameTypeRepository.VerifyGetByCalledWith(gameTypeViewModel.Id);
            mockGameTypeRepository.VerifyAddCalledWith(gameType);
            mockGameTypeMapper.VerifyMapCalledWith(gameTypeViewModel);
        }
    }
}