using BoardGameRatings.WebSite.Controllers;
using BoardGameRatings.WebSite.Tests.Mocks;
using BoardGameRatings.WebSite.ViewModels;
using Microsoft.AspNet.Mvc;
using Xunit;

namespace BoardGameRatings.WebSite.Tests.Controllers
{
    public class GamesControllerTest
    {
        [Fact]
        public void CreateANewGamesController()
        {
            var mockGamesContext = new MockGamesContext();
            var controller = new GamesController(mockGamesContext);
            Assert.NotNull(controller);
        }

        [Fact]
        public void NewRendersGames()
        {
            var gamesViewModel = new GamesViewModel();
            var mockGamesContext = new MockGamesContext().StubBuildViewModelToReturn(gamesViewModel);
            var controller = new GamesController(mockGamesContext);
            var result = (ViewResult) controller.Index();

            Assert.Equal("Index", result.ViewName);
            Assert.Equal(gamesViewModel, result.ViewData.Model);
            mockGamesContext.VerifyBuildViewModelCalled();
        }
    }
}