using System;
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
            MockCategoryMapper mockCategoryMapper = null, MockPlayedDateMapper mockPlayedGameMapper = null)
        {
            mockGameRepository = mockGameRepository ?? new MockGameRepository();
            mockCategoryRepository = mockCategoryRepository ?? new MockCategoryRepository();
            mockGameMapper = mockGameMapper ?? new MockGameMapper();
            mockCategoryMapper = mockCategoryMapper ?? new MockCategoryMapper();
            mockPlayedGameMapper = mockPlayedGameMapper ?? new MockPlayedDateMapper();
            return new GameContext(mockGameRepository, mockCategoryRepository, mockGameMapper, mockCategoryMapper, mockPlayedGameMapper);
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
            var playedDateViewModel = new PlayedDateViewModel {Id = 1, FormattedPlayedDate = "1/1/2016"};
            var playedDates = new List<PlayedDateViewModel> {playedDateViewModel};
            var game = new Game {Id = 2, Name = "Game 2", Description = "This is game 2"};
            var gamePlayedDate = new GamePlayedDate {Id = 1, GameId = game.Id, Game = game, PlayedDate = new DateTime(2016, 1, 1)};
            var gamePlayedDates = new List<GamePlayedDate> {gamePlayedDate};
            var gameViewModel = new GameViewModel
            {
                Id = 2,
                Name = "Game 2",
                Description = "This is game 2",
                Categories = categorySelectListItems,
                ElectedCategories = electedCategories,
                PlayedDates = playedDates
            };
            var mockGameRepository = new MockGameRepository()
                .StubGetAllCategoriesByToReturn(categories)
                .StubGetAllPlayedDatesByToReturn(gamePlayedDates)
                .StubGetByToReturn(game);
            var mockCategoryRepository = new MockCategoryRepository()
                .StubGetAllToReturn(categories);
            var mockGameMapper = new MockGameMapper()
                .StubMapWithCategoriesToReturn(gameViewModel);
            var mockCategoryMapper = new MockCategoryMapper()
                .StubMapToReturn(categoryViewModel)
                .StubSelectMapToReturn(item);
            var mockPlayedDateMapper = new MockPlayedDateMapper()
                .StubMapToReturn(playedDateViewModel);
            var gameContext = BuildGameContext(mockGameRepository, mockCategoryRepository, mockGameMapper, mockCategoryMapper, mockPlayedDateMapper);

            var viewModel = gameContext.BuildViewModel(game.Id);

            Assert.NotNull(viewModel);
            Assert.Equal(1, viewModel.Categories.Count());
            Assert.Equal(1, viewModel.ElectedCategories.Count());
            Assert.Equal(1, viewModel.PlayedDates.Count());
            mockGameRepository.VerifyGetByCalledWith(game.Id);
            mockGameRepository.VerifyGetAllCategoriesByCalledWith(game.Id);
            mockGameRepository.VerifyGetAllPlayedDatesByCalledWith(game.Id);
            mockCategoryRepository.VerifyGetAllCalled();
            mockGameMapper.VerifyMapCalledWith(game, categorySelectListItems, electedCategories, playedDates);
            mockCategoryMapper.VerifySelectMapCalledWith(category);
            mockCategoryMapper.VerifyMapCalledWith(category);
            mockPlayedDateMapper.VerifyMapCalledWith(gamePlayedDate);
        }


        [Fact]
        public void ContextBuildsAViewModelWithNewGame()
        {
            var category = new Category {Id = 1, Description = "Category 1"};
            var categories = new List<Category> {category};
            var item = new SelectListItem {Value = "1", Text = "Category 1"};
            var categorySelectListItems = new List<SelectListItem> {item};
            var electedCategories = new List<CategoryViewModel>();
            var playedDates = new List<PlayedDateViewModel>();
            var game = new Game();
            var gameViewModel = new GameViewModel
            {
                Categories = categorySelectListItems,
                ElectedCategories = electedCategories,
                PlayedDates = playedDates
            };
            var mockGameRepository = new MockGameRepository().StubGetByToReturn(game);
            var mockCategoryRepository = new MockCategoryRepository().StubGetAllToReturn(categories);
            var mockGameMapper = new MockGameMapper().StubMapWithCategoriesToReturn(gameViewModel);
            var mockCategoryMapper = new MockCategoryMapper().StubSelectMapToReturn(item);
            var gameContext = BuildGameContext(mockGameRepository, mockCategoryRepository, mockGameMapper,
                mockCategoryMapper);

            var viewModel = gameContext.BuildViewModel(game.Id);

            Assert.NotNull(viewModel);
            Assert.Equal(1, viewModel.Categories.Count());
            mockGameRepository.VerifyGetByCalledWith(game.Id);
            mockCategoryRepository.VerifyGetAllCalled();
            mockGameMapper.VerifyMapCalledWith(game, categorySelectListItems, electedCategories, playedDates);
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

        [Fact]
        public void ContextAddsAnElectedCategory()
        {
            var categoryId = 1;
            var gameId = 1;
            var mockGameRepository = new MockGameRepository();
            var gameContext = BuildGameContext(mockGameRepository);

            gameContext.AddElectedCategory(gameId, categoryId);

            mockGameRepository.VerifyAddElectedCategoryCalledWith(gameId, categoryId);
        }

        [Fact]
        public void ContextRemovesAnElectedCategory()
        {
            var gameId = 1;
            var categoryId = 1;
            var mockGameRepository = new MockGameRepository();
            var gameContext = BuildGameContext(mockGameRepository);

            gameContext.RemoveElectedCategory(gameId, categoryId);

            mockGameRepository.VerifyRemoveElectedCategoryCalledWith(gameId, categoryId);
        }

        [Fact]
        public void ContextAddsAPlayedDate()
        {
            var playedDate = new DateTime(2016, 1, 1);
            var gameId = 1;
            var mockGameRepository = new MockGameRepository();
            var gameContext = BuildGameContext(mockGameRepository);

            gameContext.AddPlayedDate(gameId, playedDate);

            mockGameRepository.VerifyAddPlayedDateCalledWith(gameId, playedDate);
        }

        [Fact]
        public void ContextRemovesAPlayedDate()
        {
            var gameId = 1;
            var playedDate = new DateTime(2016, 1, 1);
            var mockGameRepository = new MockGameRepository();
            var gameContext = BuildGameContext(mockGameRepository);

            gameContext.RemovePlayedDate(gameId, playedDate);

            mockGameRepository.VerifyRemovePlayedDateCalledWith(gameId, playedDate);
        }
    }
}