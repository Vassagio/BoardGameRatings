using BoardGameRatings.WebSite.Controllers;
using BoardGameRatings.WebSite.Tests.Mocks;
using BoardGameRatings.WebSite.ViewModels;
using Microsoft.AspNet.Mvc;
using Xunit;

namespace BoardGameRatings.WebSite.Tests.Controllers
{
    public class PlayerControllerTest
    {
        [Fact]
        public void CreateANewPlayersController()
        {
            var mockPlayerContext = new MockPlayerContext();
            var controller = new PlayerController(mockPlayerContext);
            Assert.NotNull(controller);
        }

        [Fact]
        public void IndexRendersAddPlayer()
        {
            var playerViewModel = new PlayerViewModel();
            var mockPlayerContext = new MockPlayerContext().StubBuildViewModelToReturn(playerViewModel);
            var controller = new PlayerController(mockPlayerContext);
            var result = (ViewResult) controller.Index();

            Assert.Equal(playerViewModel, result.ViewData.Model);
            mockPlayerContext.VerifyBuildViewModelCalledWith();
        }

        [Fact]
        public void IndexRendersUpdatePlayer()
        {
            var playerViewModel = new PlayerViewModel();
            var mockPlayerContext = new MockPlayerContext().StubBuildViewModelToReturn(playerViewModel);
            var controller = new PlayerController(mockPlayerContext);
            var result = (ViewResult) controller.Index(10);

            Assert.Equal(playerViewModel, result.ViewData.Model);
            mockPlayerContext.VerifyBuildViewModelCalledWith(10);
        }

        [Fact]
        public void SavesThePlayer()
        {
            var playerViewModel = new PlayerViewModel();
            var mockPlayerContext = new MockPlayerContext();
            var controller = new PlayerController(mockPlayerContext);
            var result = controller.Save(playerViewModel);

            Assert.IsType<RedirectToRouteResult>(result);
            Assert.Equal("Index", result.RouteValues["action"]);
            Assert.Equal("Players", result.RouteValues["controller"]);
            mockPlayerContext.VerifySaveCalledWith(playerViewModel);
        }

        [Fact]
        public void AddsAnOwnedGame()
        {
            var playerViewModel = new PlayerViewModel
            {
                Id = 1,
                GameId = 1
            };
            var mockPlayerContext = new MockPlayerContext();
            var controller = new PlayerController(mockPlayerContext);
            var result = controller.Add(playerViewModel);

            Assert.IsType<RedirectToRouteResult>(result);
            Assert.Equal("Index", result.RouteValues["action"]);
            Assert.Equal("Player", result.RouteValues["controller"]);
            mockPlayerContext.VerifyAddGameOwnedCalledWith(1, 1);
        }

        [Fact]
        public void RemovesAnOwnedGame()
        {
            var playerId = 1;
            var gameId = 1;            
            var mockPlayerContext = new MockPlayerContext();
            var controller = new PlayerController(mockPlayerContext);
            var result = controller.Remove(playerId, gameId);

            Assert.IsType<RedirectToRouteResult>(result);
            Assert.Equal("Index", result.RouteValues["action"]);
            Assert.Equal("Player", result.RouteValues["controller"]);
            mockPlayerContext.VerifyRemoveGameOwnedCalledWith(1, 1);
        }
    }
}