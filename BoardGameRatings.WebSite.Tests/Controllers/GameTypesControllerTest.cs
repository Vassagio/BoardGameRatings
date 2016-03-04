using BoardGameRatings.WebSite.Controllers;
using BoardGameRatings.WebSite.Tests.Mocks;
using BoardGameRatings.WebSite.ViewModels;
using Microsoft.AspNet.Mvc;
using Xunit;

namespace BoardGameRatings.WebSite.Tests.Controllers
{
    public class GameTypesControllerTest
    {
        [Fact]
        public void CreateANewGameTypesController()
        {
            var mockGameTypesContext = new MockGameTypesContext();
            var controller = new GameTypesController(mockGameTypesContext);
            Assert.NotNull(controller);
        }

        [Fact]
        public void IndexRendersGameTypes()
        {
            var gameTypesViewModel = new GameTypesViewModel();
            var mockGameTypesContext = new MockGameTypesContext().StubBuildViewModelToReturn(gameTypesViewModel);
            var controller = new GameTypesController(mockGameTypesContext);
            var result = (ViewResult) controller.Index();

            Assert.Equal(gameTypesViewModel, result.ViewData.Model);
            mockGameTypesContext.VerifyBuildViewModelCalled();
        }

        [Fact]
        public void RemoveRedirectsToTheIndexView()
        {
            var mockGameTypesContext = new MockGameTypesContext();
            var controller = new GameTypesController(mockGameTypesContext);
            var result = controller.Remove(10);

            Assert.IsType<RedirectToRouteResult>(result);
            Assert.Equal("Index", result.RouteValues["action"]);
            Assert.Equal("GameTypes", result.RouteValues["controller"]);
            mockGameTypesContext.VerifyRemoveCalledWith(10);
        }

        [Fact]
        public void EditRedirectsToTheGameTypeIndexView()
        {
            var id = 5;
            var mockGameTypesContext = new MockGameTypesContext();
            var controller = new GameTypesController(mockGameTypesContext);
            var result = controller.Edit(id);

            Assert.IsType<RedirectToRouteResult>(result);
            Assert.Equal("Index", result.RouteValues["action"]);
            Assert.Equal("GameType", result.RouteValues["controller"]);
            Assert.Equal(id, result.RouteValues["id"]);
        }

        [Fact]
        public void AddRedirectsToTheGameTypeIndexView()
        {
            var mockGameTypesContext = new MockGameTypesContext();
            var controller = new GameTypesController(mockGameTypesContext);
            var result = controller.Add();

            Assert.IsType<RedirectToRouteResult>(result);
            Assert.Equal("Index", result.RouteValues["action"]);
            Assert.Equal("GameType", result.RouteValues["controller"]);
        }
    }
}