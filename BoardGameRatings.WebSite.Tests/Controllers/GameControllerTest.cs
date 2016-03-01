using BoardGameRatings.WebSite.Controllers;
using BoardGameRatings.WebSite.Tests.Mocks;
using BoardGameRatings.WebSite.ViewModels;
using Microsoft.AspNet.Mvc;
using Xunit;

namespace BoardGameRatings.WebSite.Tests.Controllers
{
    public class GameControllerTest
    {
        [Fact]
        public void CreateANewGamesController()
        {
            var mockGameContext = new MockGameContext();
            var controller = new GameController(mockGameContext);
            Assert.NotNull(controller);
        }

        [Fact]
        public void IndexRendersAddGame()
        {
            var gameViewModel = new GameViewModel();
            var mockGameContext = new MockGameContext().StubBuildViewModelToReturn(gameViewModel);
            var controller = new GameController(mockGameContext);
            var result = (ViewResult) controller.Index();

            Assert.Equal(gameViewModel, result.ViewData.Model);
            mockGameContext.VerifyBuildViewModelCalledWith();
        }

        [Fact]
        public void IndexRendersUpdateGame()
        {
            var gameViewModel = new GameViewModel();
            var mockGameContext = new MockGameContext().StubBuildViewModelToReturn(gameViewModel);
            var controller = new GameController(mockGameContext);
            var result = (ViewResult) controller.Index(10);

            Assert.Equal(gameViewModel, result.ViewData.Model);
            mockGameContext.VerifyBuildViewModelCalledWith(10);
        }

        [Fact]
        public void SavesTheGame()
        {
            var gameViewModel = new GameViewModel();
            var mockGameContext = new MockGameContext();
            var controller = new GameController(mockGameContext);
            var result = controller.Save(gameViewModel);

            Assert.IsType<RedirectToRouteResult>(result);
            Assert.Equal("Index", result.RouteValues["action"]);
            Assert.Equal("Games", result.RouteValues["controller"]);
            mockGameContext.VerifySaveCalledWith(gameViewModel);
        }
    }
}