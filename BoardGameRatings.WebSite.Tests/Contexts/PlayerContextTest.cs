using BoardGameRatings.WebSite.Contexts;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.Tests.Mocks;
using BoardGameRatings.WebSite.ViewModels;
using Xunit;

namespace BoardGameRatings.WebSite.Tests.Contexts
{
    public class PlayerContextTest
    {
        [Fact]
        public void CreatesAPlayerContext()
        {
            var mockPlayerRepository = new MockPlayerRepository();
            var mockPlayerMapper = new MockPlayerMapper();
            var playerContext = new PlayerContext(mockPlayerRepository, mockPlayerMapper);

            Assert.NotNull(playerContext);
        }

        [Fact]
        public void ContextBuildsAViewModelWithEditedPlayer()
        {
            var player = new Player {Id = 2, FirstName = "First 2", LastName = "Last 2"};
            var playerViewModel = new PlayerViewModel {Id = 2, FirstName = "First 2", LastName = "Last 2"};
            var mockPlayerRepository = new MockPlayerRepository().StubGetByIdToReturn(player);
            var mockPlayerMapper = new MockPlayerMapper().StubMapToReturn(playerViewModel);
            var playerContext = new PlayerContext(mockPlayerRepository, mockPlayerMapper);

            var viewModel = playerContext.BuildViewModel(player.Id);

            Assert.NotNull(viewModel);
            Assert.Equal("First 1 Last 1", viewModel.GetFullName());
            mockPlayerRepository.VerifyGetByIdCalledWith(player.Id);
            mockPlayerMapper.VerifyMapCalledWith(player);
        }


        [Fact]
        public void ContextBuildsAViewModelWithNewPlayer()
        {
            var mockPlayerRepository = new MockPlayerRepository();
            var mockPlayerMapper = new MockPlayerMapper();
            var playerContext = new PlayerContext(mockPlayerRepository, mockPlayerMapper);

            var viewModel = playerContext.BuildViewModel();

            Assert.NotNull(viewModel);
            Assert.Equal(string.Empty, viewModel.GetFullName());
        }

        [Fact]
        public void ContextSavesAnUpdatedPlayer()
        {
            var player = new Player {Id = 2, FirstName = "First 2", LastName= "Last 2"};
            var playerViewModel = new PlayerViewModel
            {
                Id = 2,
                FirstName = "Updated First",
                LastName = "Updated Last"
            };
            var mockPlayerRepository = new MockPlayerRepository().StubGetByIdToReturn(player);
            var mockPlayerMapper = new MockPlayerMapper();
            var playerContext = new PlayerContext(mockPlayerRepository, mockPlayerMapper);

            playerContext.Save(playerViewModel);

            mockPlayerRepository.VerifyGetByIdCalledWith(playerViewModel.Id);
            mockPlayerRepository.VerifyUpdateCalledWith(player);
        }

        [Fact]
        public void ContextSavesANewPlayer()
        {
            var player = new Player {Id = 4, FirstName = "New First", LastName = "New Last"};
            var playerViewModel = new PlayerViewModel {Id = 4, FirstName = "New First", LastName= "New Last"};
            var mockPlayerRepository = new MockPlayerRepository();
            var mockPlayerMapper = new MockPlayerMapper().StubMapToReturn(player);
            var playerContext = new PlayerContext(mockPlayerRepository, mockPlayerMapper);

            playerContext.Save(playerViewModel);

            mockPlayerRepository.VerifyGetByIdCalledWith(playerViewModel.Id);
            mockPlayerRepository.VerifyAddCalledWith(player);
            mockPlayerMapper.VerifyMapCalledWith(playerViewModel);
        }
    }
}