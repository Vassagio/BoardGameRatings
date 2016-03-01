using BoardGameRatings.WebSite.Controllers;
using BoardGameRatings.WebSite.Tests.Mocks;
using BoardGameRatings.WebSite.ViewModels;
using Microsoft.AspNet.Mvc;
using Xunit;

namespace BoardGameRatings.WebSite.Tests.Controllers
{
    public class PlayersControllerTest
    {
        [Fact]
        public void CreateANewPlayersController()
        {
            var mockPlayersContext = new MockPlayersContext();
            var controller = new PlayersController(mockPlayersContext);
            Assert.NotNull(controller);
        }

        [Fact]
        public void IndexRendersPlayers()
        {
            var playersViewModel = new PlayersViewModel();
            var mockPlayersContext = new MockPlayersContext().StubBuildViewModelToReturn(playersViewModel);
            var controller = new PlayersController(mockPlayersContext);
            var result = (ViewResult) controller.Index();

            Assert.Equal(playersViewModel, result.ViewData.Model);
            mockPlayersContext.VerifyBuildViewModelCalled();
        }

        [Fact]
        public void RemoveRedirectsToTheIndexView()
        {
            var mockPlayersContext = new MockPlayersContext();
            var controller = new PlayersController(mockPlayersContext);
            var result = controller.Remove(10);

            Assert.IsType<RedirectToRouteResult>(result);
            Assert.Equal("Index", result.RouteValues["action"]);
            Assert.Equal("Players", result.RouteValues["controller"]);
            mockPlayersContext.VerifyRemoveCalledWith(10);
        }

        [Fact]
        public void EditRedirectsToThePlayerIndexView()
        {
            var id = 5;
            var mockPlayersContext = new MockPlayersContext();
            var controller = new PlayersController(mockPlayersContext);
            var result = controller.Edit(id);

            Assert.IsType<RedirectToRouteResult>(result);
            Assert.Equal("Index", result.RouteValues["action"]);
            Assert.Equal("Player", result.RouteValues["controller"]);
            Assert.Equal(id, result.RouteValues["id"]);
        }

        [Fact]
        public void AddRedirectsToThePlayerIndexView()
        {
            var mockPlayersContext = new MockPlayersContext();
            var controller = new PlayersController(mockPlayersContext);
            var result = controller.Add();

            Assert.IsType<RedirectToRouteResult>(result);
            Assert.Equal("Index", result.RouteValues["action"]);
            Assert.Equal("Player", result.RouteValues["controller"]);
        }
    }
}