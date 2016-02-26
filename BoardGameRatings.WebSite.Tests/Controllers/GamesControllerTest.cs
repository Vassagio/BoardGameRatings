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
        public void IndexRendersGames()
        {
            var gamesViewModel = new GamesViewModel();
            var mockGamesContext = new MockGamesContext().StubBuildViewModelToReturn(gamesViewModel);
            var controller = new GamesController(mockGamesContext);
            var result = (ViewResult) controller.Index();

            Assert.Equal(gamesViewModel, result.ViewData.Model);
            mockGamesContext.VerifyBuildViewModelCalled();
        }

        [Fact]
        public void RemoveRedirectsToTheIndexView()
        {
            var mockGamesContext = new MockGamesContext();
            var controller = new GamesController(mockGamesContext);
            var result = controller.Remove(10);

            Assert.IsType<RedirectToRouteResult>(result);
            Assert.Equal("Index", result.RouteValues["action"]);
            Assert.Equal("Games", result.RouteValues["controller"]);
            mockGamesContext.VerifyRemoveCalledWith(10);
        }
    }
}