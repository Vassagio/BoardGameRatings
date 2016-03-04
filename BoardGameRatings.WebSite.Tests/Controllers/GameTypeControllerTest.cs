using BoardGameRatings.WebSite.Controllers;
using BoardGameRatings.WebSite.Tests.Mocks;
using BoardGameRatings.WebSite.ViewModels;
using Microsoft.AspNet.Mvc;
using Xunit;

namespace BoardGameRatings.WebSite.Tests.Controllers
{
    public class GameTypeControllerTest
    {
        [Fact]
        public void CreateANewGameTypesController()
        {
            var mockGameTypeContext = new MockGameTypeContext();
            var controller = new GameTypeController(mockGameTypeContext);
            Assert.NotNull(controller);
        }

        [Fact]
        public void IndexRendersAddGameType()
        {
            var gameTypeViewModel = new GameTypeViewModel();
            var mockGameTypeContext = new MockGameTypeContext().StubBuildViewModelToReturn(gameTypeViewModel);
            var controller = new GameTypeController(mockGameTypeContext);
            var result = (ViewResult) controller.Index();

            Assert.Equal(gameTypeViewModel, result.ViewData.Model);
            mockGameTypeContext.VerifyBuildViewModelCalledWith();
        }

        [Fact]
        public void IndexRendersUpdateGameType()
        {
            var gameTypeViewModel = new GameTypeViewModel();
            var mockGameTypeContext = new MockGameTypeContext().StubBuildViewModelToReturn(gameTypeViewModel);
            var controller = new GameTypeController(mockGameTypeContext);
            var result = (ViewResult) controller.Index(10);

            Assert.Equal(gameTypeViewModel, result.ViewData.Model);
            mockGameTypeContext.VerifyBuildViewModelCalledWith(10);
        }

        [Fact]
        public void SavesTheGameType()
        {
            var gameTypeViewModel = new GameTypeViewModel();
            var mockGameTypeContext = new MockGameTypeContext();
            var controller = new GameTypeController(mockGameTypeContext);
            var result = controller.Save(gameTypeViewModel);

            Assert.IsType<RedirectToRouteResult>(result);
            Assert.Equal("Index", result.RouteValues["action"]);
            Assert.Equal("GameTypes", result.RouteValues["controller"]);
            mockGameTypeContext.VerifySaveCalledWith(gameTypeViewModel);
        }
    }
}