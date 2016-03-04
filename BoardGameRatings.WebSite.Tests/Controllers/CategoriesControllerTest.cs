using BoardGameRatings.WebSite.Controllers;
using BoardGameRatings.WebSite.Tests.Mocks;
using BoardGameRatings.WebSite.ViewModels;
using Microsoft.AspNet.Mvc;
using Xunit;

namespace BoardGameRatings.WebSite.Tests.Controllers
{
    public class CategoriesControllerTest
    {
        [Fact]
        public void CreateANewCategoriesController()
        {
            var mockCategoriesContext = new MockCategoriesContext();
            var controller = new CategoriesController(mockCategoriesContext);
            Assert.NotNull(controller);
        }

        [Fact]
        public void IndexRendersCategories()
        {
            var categoriesViewModel = new CategoriesViewModel();
            var mockCategoriesContext = new MockCategoriesContext().StubBuildViewModelToReturn(categoriesViewModel);
            var controller = new CategoriesController(mockCategoriesContext);
            var result = (ViewResult) controller.Index();

            Assert.Equal(categoriesViewModel, result.ViewData.Model);
            mockCategoriesContext.VerifyBuildViewModelCalled();
        }

        [Fact]
        public void RemoveRedirectsToTheIndexView()
        {
            var mockCategoriesContext = new MockCategoriesContext();
            var controller = new CategoriesController(mockCategoriesContext);
            var result = controller.Remove(10);

            Assert.IsType<RedirectToRouteResult>(result);
            Assert.Equal("Index", result.RouteValues["action"]);
            Assert.Equal("Categories", result.RouteValues["controller"]);
            mockCategoriesContext.VerifyRemoveCalledWith(10);
        }

        [Fact]
        public void EditRedirectsToTheCategoryIndexView()
        {
            var id = 5;
            var mockCategoriesContext = new MockCategoriesContext();
            var controller = new CategoriesController(mockCategoriesContext);
            var result = controller.Edit(id);

            Assert.IsType<RedirectToRouteResult>(result);
            Assert.Equal("Index", result.RouteValues["action"]);
            Assert.Equal("Category", result.RouteValues["controller"]);
            Assert.Equal(id, result.RouteValues["id"]);
        }

        [Fact]
        public void AddRedirectsToTheCategoryIndexView()
        {
            var mockCategoriesContext = new MockCategoriesContext();
            var controller = new CategoriesController(mockCategoriesContext);
            var result = controller.Add();

            Assert.IsType<RedirectToRouteResult>(result);
            Assert.Equal("Index", result.RouteValues["action"]);
            Assert.Equal("Category", result.RouteValues["controller"]);
        }
    }
}