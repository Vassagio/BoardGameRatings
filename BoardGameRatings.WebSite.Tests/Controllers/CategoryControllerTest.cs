using BoardGameRatings.WebSite.Controllers;
using BoardGameRatings.WebSite.Tests.Mocks;
using BoardGameRatings.WebSite.ViewModels;
using Microsoft.AspNet.Mvc;
using Xunit;

namespace BoardGameRatings.WebSite.Tests.Controllers
{
    public class CategoryControllerTest
    {
        [Fact]
        public void CreateANewCategoriesController()
        {
            var mockCategoryContext = new MockCategoryContext();
            var controller = new CategoryController(mockCategoryContext);
            Assert.NotNull(controller);
        }

        [Fact]
        public void IndexRendersAddCategory()
        {
            var categoryViewModel = new CategoryViewModel();
            var mockCategoryContext = new MockCategoryContext().StubBuildViewModelToReturn(categoryViewModel);
            var controller = new CategoryController(mockCategoryContext);
            var result = (ViewResult) controller.Index();

            Assert.Equal(categoryViewModel, result.ViewData.Model);
            mockCategoryContext.VerifyBuildViewModelCalledWith();
        }

        [Fact]
        public void IndexRendersUpdateCategory()
        {
            var categoryViewModel = new CategoryViewModel();
            var mockCategoryContext = new MockCategoryContext().StubBuildViewModelToReturn(categoryViewModel);
            var controller = new CategoryController(mockCategoryContext);
            var result = (ViewResult) controller.Index(10);

            Assert.Equal(categoryViewModel, result.ViewData.Model);
            mockCategoryContext.VerifyBuildViewModelCalledWith(10);
        }

        [Fact]
        public void SavesTheCategory()
        {
            var categoryViewModel = new CategoryViewModel();
            var mockCategoryContext = new MockCategoryContext();
            var controller = new CategoryController(mockCategoryContext);
            var result = controller.Save(categoryViewModel);

            Assert.IsType<RedirectToRouteResult>(result);
            Assert.Equal("Index", result.RouteValues["action"]);
            Assert.Equal("Categories", result.RouteValues["controller"]);
            mockCategoryContext.VerifySaveCalledWith(categoryViewModel);
        }
    }
}