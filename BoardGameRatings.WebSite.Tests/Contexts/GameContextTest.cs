using System.Collections.Generic;
using System.Linq;
using BoardGameRatings.WebSite.Contexts;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.Tests.Mocks;
using BoardGameRatings.WebSite.ViewModels;
using Microsoft.AspNet.Mvc.Rendering;
using Xunit;

namespace BoardGameRatings.WebSite.Tests.Contexts
{
    public class GameContextTest
    {
        [Fact]
        public void CreatesAGameContext()
        {
            var gameContext = BuildGameContext();

            Assert.NotNull(gameContext);
        }

        private GameContext BuildGameContext(MockGameRepository mockGameRepository = null,
            MockCategoryRepository mockCategoryRepository = null, MockGameMapper mockGameMapper = null,
            MockCategoryMapper mockCategoryMapper = null)
        {
            mockGameRepository = mockGameRepository ?? new MockGameRepository();
            mockCategoryRepository = mockCategoryRepository ?? new MockCategoryRepository();
            mockGameMapper = mockGameMapper ?? new MockGameMapper();
            mockCategoryMapper = mockCategoryMapper ?? new MockCategoryMapper();
            return new GameContext(mockGameRepository, mockCategoryRepository, mockGameMapper, mockCategoryMapper);
        }

        [Fact]
        public void ContextBuildsAViewModelWithEditedGame()
        {
            var category = new Category {Id = 1, Description = "Category 1"};
            var categories = new List<Category> {category};
            var item = new SelectListItem {Value = "1", Text = "Category 1"};
            var categorySelectListItems = new List<SelectListItem> {item};
            var categoryViewModel = new CategoryViewModel {Id = 1, Description = "Category 1"};
            var electedCategories = new List<CategoryViewModel> {categoryViewModel};
            var game = new Game {Id = 2, Name = "Game 2", Description = "This is game 2"};
            var gameViewModel = new GameViewModel
            {
                Id = 2,
                Name = "Game 2",
                Description = "This is game 2",
                Categories = categorySelectListItems,
                ElectedCategories = electedCategories
            };
            var mockGameRepository =
                new MockGameRepository().StubGetAllCategoriesByToReturn(categories).StubGetByToReturn(game);
            var mockCategoryRepository = new MockCategoryRepository().StubGetAllToReturn(categories);
            var mockGameMapper = new MockGameMapper().StubMapWithCategoriesToReturn(gameViewModel);
            var mockCategoryMapper =
                new MockCategoryMapper().StubMapToReturn(categoryViewModel).StubSelectMapToReturn(item);
            var gameContext = new GameContext(mockGameRepository, mockCategoryRepository, mockGameMapper,
                mockCategoryMapper);

            var viewModel = gameContext.BuildViewModel(game.Id);

            Assert.NotNull(viewModel);
            Assert.Equal(1, viewModel.Categories.Count());
            Assert.Equal(1, viewModel.ElectedCategories.Count());
            mockGameRepository.VerifyGetByCalledWith(game.Id);
            mockGameRepository.VerifyGetAllCategoriesByCalledWith(game.Id);
            mockCategoryRepository.VerifyGetAllCalled();
            mockGameMapper.VerifyMapCalledWith(game, categorySelectListItems, electedCategories);
            mockCategoryMapper.VerifySelectMapCalledWith(category);
            mockCategoryMapper.VerifyMapCalledWith(category);
        }


        [Fact]
        public void ContextBuildsAViewModelWithNewGame()
        {
            var category = new Category {Id = 1, Description = "Category 1"};
            var categories = new List<Category> {category};
            var item = new SelectListItem {Value = "1", Text = "Category 1"};
            var categorySelectListItems = new List<SelectListItem> {item};
            var electedCategories = new List<CategoryViewModel>();
            var game = new Game();
            var gameViewModel = new GameViewModel
            {
                Categories = categorySelectListItems,
                ElectedCategories = electedCategories
            };
            var mockGameRepository = new MockGameRepository().StubGetByToReturn(game);
            var mockCategoryRepository = new MockCategoryRepository().StubGetAllToReturn(categories);
            var mockGameMapper = new MockGameMapper().StubMapWithCategoriesToReturn(gameViewModel);
            var mockCategoryMapper = new MockCategoryMapper().StubSelectMapToReturn(item);
            var gameContext = new GameContext(mockGameRepository, mockCategoryRepository, mockGameMapper,
                mockCategoryMapper);

            var viewModel = gameContext.BuildViewModel(game.Id);

            Assert.NotNull(viewModel);
            Assert.Equal(1, viewModel.Categories.Count());
            mockGameRepository.VerifyGetByCalledWith(game.Id);
            mockCategoryRepository.VerifyGetAllCalled();
            mockGameMapper.VerifyMapCalledWith(game, categorySelectListItems, electedCategories);
            mockCategoryMapper.VerifySelectMapCalledWith(category);
        }

        [Fact]
        public void ContextSavesAnUpdatedGame()
        {
            var game = new Game {Id = 2, Name = "Game 2", Description = "This is game 2"};
            var gameViewModel = new GameViewModel
            {
                Id = 2,
                Name = "Updated Game",
                Description = "This is an updated game"
            };
            var mockGameRepository = new MockGameRepository().StubGetByToReturn(game);
            var mockGameMapper = new MockGameMapper();
            var gameContext = BuildGameContext(mockGameRepository, mockGameMapper: mockGameMapper);

            gameContext.Save(gameViewModel);

            mockGameRepository.VerifyGetByCalledWith(gameViewModel.Id);
            mockGameRepository.VerifyUpdateCalledWith(game);
        }

        [Fact]
        public void ContextSavesANewGame()
        {
            var game = new Game {Id = 4, Name = "New Game", Description = "This is new game"};
            var gameViewModel = new GameViewModel {Id = 4, Name = "New Game", Description = "This is a new game"};
            var mockGameRepository = new MockGameRepository();
            var mockGameMapper = new MockGameMapper().StubMapToReturn(game);
            var gameContext = BuildGameContext(mockGameRepository, mockGameMapper: mockGameMapper);

            gameContext.Save(gameViewModel);

            mockGameRepository.VerifyGetByCalledWith(gameViewModel.Id);
            mockGameRepository.VerifyAddCalledWith(game);
            mockGameMapper.VerifyMapCalledWith(gameViewModel);
        }
    }
}