using System;
using System.Collections.Generic;
using System.Linq;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.Models.Repositories;
using BoardGameRatings.WebSite.Tests.Extensions;
using Xunit;

namespace BoardGameRatings.WebSite.Tests.Models.Repositories
{
    public class GameRepositoryTest : IClassFixture<RepositoryFixture>, IDisposable
    {
        private readonly RepositoryFixture _fixture;

        public GameRepositoryTest(RepositoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.Begin();
        }

        public void Dispose()
        {
            _fixture.End();
        }

        [Fact]
        public void CreateANewGameRepository()
        {
            var gameRepository = new GameRepository(_fixture.Context);
            Assert.NotNull(gameRepository);
        }

        [Fact]
        public void GetAllGames()
        {
            var games = new List<Game>
            {
                new Game {Name = "Game 1"},
                new Game {Name = "Game 2"},
                new Game {Name = "Game 3"}
            };

            var gameRepository = new GameRepository(_fixture.Context.GamesContain(games));

            var result = gameRepository.GetAll()
                .ToList();

            Assert.Equal(3, result.Count());
            Assert.Equal(games, result.OrderBy(r => r.Id));
        }

        [Fact]
        public void AddGame()
        {
            var game = new Game
            {
                Name = "Game 1"
            };

            var gameRepository = new GameRepository(_fixture.Context);

            var result = gameRepository.Add(game);

            Assert.NotNull(result);
            Assert.Equal(game, result);
        }

        [Fact]
        public void DoesNotAddDuplicateGame()
        {
            var game = new Game
            {
                Name = "Game 1"
            };

            var gameRepository = new GameRepository(_fixture.Context);

            gameRepository.Add(game);
            gameRepository.Add(game);

            var result = gameRepository.GetAll();

            Assert.Equal(1, result.Count());
            Assert.Equal(game, result.First());
        }

        [Fact]
        public void RemoveGame()
        {
            var game1 = new Game {Name = "Game 1"};
            var game2 = new Game {Name = "Game 2"};
            var game3 = new Game {Name = "Game 3"};
            var games = new List<Game> {game1, game2, game3};

            var gameRepository = new GameRepository(_fixture.Context.GamesContain(games));

            gameRepository.Remove(game2);
            var result = gameRepository.GetAll()
                .ToList();

            Assert.Equal(2, result.Count());
            Assert.Equal(new List<Game> {game1, game3}, result.OrderBy(r => r.Id));
        }

        [Fact]
        public void GetGameById()
        {
            var game3 = new Game {Name = "Game 3"};
            var games = new List<Game>
            {
                new Game {Name = "Game 1"},
                new Game {Name = "Game 2"},
                game3
            };

            var gameRepository = new GameRepository(_fixture.Context.GamesContain(games));

            var result = gameRepository.GetBy(game3.Id);

            Assert.Equal(game3.Id, result.Id);
            Assert.Equal(game3.Name, result.Name);
            Assert.Equal(game3.Description, result.Description);
        }

        [Theory]
        [InlineData("Game 3")]
        [InlineData("game 3")]
        public void GetGameByName(string name)
        {
            var game3 = new Game {Name = name};
            var games = new List<Game>
            {
                new Game {Name = "Game 1"},
                new Game {Name = "Game 2"},
                game3
            };

            var gameRepository = new GameRepository(_fixture.Context.GamesContain(games));

            var result = gameRepository.GetBy(game3.Name);

            Assert.Equal(game3.Id, result.Id);
            Assert.Equal(game3.Name, result.Name);
            Assert.Equal(game3.Description, result.Description);
        }

        [Fact]
        public void UpdateGame()
        {
            var games = new List<Game>
            {
                new Game {Name = "Game 1"},
                new Game {Name = "Game 2"},
                new Game {Name = "Game 3"}
            };

            var gameRepository = new GameRepository(_fixture.Context.GamesContain(games));

            var game = gameRepository.GetBy(1);
            game.Description = "Battleship boardgame";
            gameRepository.Update(game);
            var result = gameRepository.GetBy(1);

            Assert.Equal(1, result.Id);
            Assert.Equal("Game 1", game.Name);
            Assert.Equal("Battleship boardgame", game.Description);
        }

        [Fact]
        public void GetAllElectedCategoriesByGame()
        {
            var games = new List<Game>
            {
                new Game {Id = 1, Name = "Game 1"},
                new Game {Id = 2, Name = "Game 2"}
            };

            var categories = new List<Category>
            {
                new Category {Id = 1, Description = "Category 1"},
                new Category {Id = 2, Description = "Category 2"},
                new Category {Id = 3, Description = "Category 3"}
            };

            var gameCategories = new List<GameCategory>
            {
                new GameCategory {CategoryId = 1, GameId = 1},
                new GameCategory {CategoryId = 2, GameId = 1},
                new GameCategory {CategoryId = 3, GameId = 1},
                new GameCategory {CategoryId = 2, GameId = 2}
            };

            var context = _fixture.Context
                .GamesContain(games)
                .CategoriesContain(categories)
                .GameCategoriesContain(gameCategories);
            var gameRepository = new GameRepository(context);

            var resultGame1 = gameRepository.GetAllCategoriesBy(1);
            var resultGame2 = gameRepository.GetAllCategoriesBy(2);

            Assert.Equal(3, resultGame1.Count());
            Assert.Equal(1, resultGame2.Count());
        }

        [Fact]
        public void GetElectedCategoriesBy()
        {
            var games = new List<Game>
            {
                new Game {Id = 1, Name = "Game 1"},
                new Game {Id = 2, Name = "Game 2"}
            };

            var categories = new List<Category>
            {
                new Category {Id = 1, Description = "Category 1"},
                new Category {Id = 2, Description = "Category 2"},
                new Category {Id = 3, Description = "Category 3"}
            };

            var gameCategories = new List<GameCategory>
            {
                new GameCategory {CategoryId = 1, GameId = 1},
                new GameCategory {CategoryId = 2, GameId = 1},
                new GameCategory {CategoryId = 3, GameId = 1},
                new GameCategory {CategoryId = 2, GameId = 2}
            };

            var context = _fixture.Context
                .GamesContain(games)
                .CategoriesContain(categories)
                .GameCategoriesContain(gameCategories);
            var gameRepository = new GameRepository(context);

            var resultGameCategory = gameRepository.GetGameCategoryBy(1, 2);

            Assert.Equal(2, resultGameCategory.Id);
            Assert.Equal(1, resultGameCategory.GameId);
            Assert.Equal(2, resultGameCategory.CategoryId);
        }

        [Fact]
        public void AddElectedCategory()
        {
            var game = new Game {Id = 1, Name = "Game 1"};
            var games = new List<Game> {game};
            var category = new Category {Id = 1, Description = "Category 1"};
            var categories = new List<Category> {category};

            var context = _fixture.Context
                .GamesContain(games)
                .CategoriesContain(categories);
            var gameRepository = new GameRepository(context);

            gameRepository.AddElectedCategory(game.Id, category.Id);

            var result = gameRepository.GetAllCategoriesBy(game.Id);

            Assert.Equal(1, result.First()
                .Id);
            Assert.Equal("Category 1", result.First()
                .Description);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        public void DoesNotAddInvalidElectedCategory(int gameId, int categoryId)
        {
            var game = new Game {Id = gameId, Name = "Game 1"};
            var games = gameId == 0 ? new List<Game>() : new List<Game> {game};
            var category = new Category {Id = categoryId, Description = "Category 1"};
            var categories = categoryId == 0 ? new List<Category>() : new List<Category> {category};

            var context = _fixture.Context
                .GamesContain(games)
                .CategoriesContain(categories);
            var gameRepository = new GameRepository(context);

            Assert.Throws<ArgumentException>(() => gameRepository.AddElectedCategory(gameId, categoryId));
        }

        [Fact]
        public void RemoveElectedCategory()
        {
            var category = new Category {Id = 1, Description = "Category 1"};
            var categories = new List<Category> {category};
            var game = new Game {Id = 1, Name = "Game 1"};
            var games = new List<Game> {game};
            var electedCategory = new GameCategory {GameId = game.Id, CategoryId = category.Id};
            var electedCategories = new List<GameCategory> {electedCategory};

            var context = _fixture.Context
                .CategoriesContain(categories)
                .GamesContain(games)
                .GameCategoriesContain(electedCategories);
            var gameRepository = new GameRepository(context);

            gameRepository.RemoveElectedCategory(game.Id, category.Id);

            var result = gameRepository.GetAllCategoriesBy(category.Id);

            Assert.False(result.Any());
        }

        [Fact]
        public void DoesNotAddDuplicateElectedCategory()
        {
            var game = new Game {Id = 1, Name = "Game 1"};
            var games = new List<Game> {game};
            var category = new Category {Id = 1, Description = "Category 1"};
            var categories = new List<Category> {category};

            var context = _fixture.Context
                .GamesContain(games)
                .CategoriesContain(categories);
            var gameRepository = new GameRepository(context);

            gameRepository.AddElectedCategory(game.Id, category.Id);
            Assert.Throws<ArgumentException>(() => gameRepository.AddElectedCategory(game.Id, category.Id));
        }

        [Fact]
        public void GetAllPlayedDatesByGame()
        {
            var games = new List<Game>
            {
                new Game {Id = 1, Name = "Game 1"},
                new Game {Id = 2, Name = "Game 2"}
            };

            var gamePlayedDates = new List<GamePlayedDate>
            {
                new GamePlayedDate {PlayedDate = new DateTime(2016, 1, 1), GameId = 1},
                new GamePlayedDate {PlayedDate = new DateTime(2016, 2, 2), GameId = 1},
                new GamePlayedDate {PlayedDate = new DateTime(2016, 1, 1), GameId = 2},
                new GamePlayedDate {PlayedDate = new DateTime(2016, 3, 3), GameId = 2}
            };

            var context = _fixture.Context
                .GamesContain(games)
                .GamePlayedDatesContain(gamePlayedDates);
            var gameRepository = new GameRepository(context);

            var resultGame1 = gameRepository.GetAllPlayedDatesBy(1);
            var resultGame2 = gameRepository.GetAllPlayedDatesBy(2);

            Assert.Equal(2, resultGame1.Count());
            Assert.Equal(2, resultGame2.Count());
        }

        [Fact]
        public void GetPlayedDatesBy()
        {
            var playedDate = new DateTime(2016, 1, 1);
            var games = new List<Game>
            {
                new Game {Id = 1, Name = "Game 1"},
                new Game {Id = 2, Name = "Game 2"}
            };

            var gamePlayedDates = new List<GamePlayedDate>
            {
                new GamePlayedDate {PlayedDate = playedDate, GameId = 1},
                new GamePlayedDate {PlayedDate = new DateTime(2016, 2, 2), GameId = 1},
                new GamePlayedDate {PlayedDate = playedDate, GameId = 2},
                new GamePlayedDate {PlayedDate = new DateTime(2016, 3, 3), GameId = 2}
            };

            var context = _fixture.Context
                .GamesContain(games)
                .GamePlayedDatesContain(gamePlayedDates);
            var gameRepository = new GameRepository(context);

            var resultGame = gameRepository.GetGamePlayedDateBy(1, playedDate);

            Assert.Equal(1, resultGame.Id);
            Assert.Equal(1, resultGame.GameId);
            Assert.Equal(playedDate, resultGame.PlayedDate);
        }

        [Fact]
        public void AddPlayedDate()
        {
            var game = new Game {Id = 1, Name = "Game 1"};
            var games = new List<Game> {game};
            var playedDate = new DateTime(2016, 1, 1);

            var context = _fixture.Context
                .GamesContain(games);
            var gameRepository = new GameRepository(context);

            gameRepository.AddPlayedDate(game.Id, playedDate);

            var result = gameRepository.GetAllPlayedDatesBy(game.Id);

            Assert.Equal(playedDate, result.First()
                .PlayedDate);
        }

        [Fact]
        public void DoesNotAddInvalidDatePlayed()
        {
            var gameId = 0;
            var playedDate = new DateTime(2016, 1, 1);

            var context = _fixture.Context
                .GamesContain(new List<Game>());
            var gameRepository = new GameRepository(context);

            Assert.Throws<ArgumentException>(() => gameRepository.AddPlayedDate(gameId, playedDate));
        }

        [Fact]
        public void RemovePlayedGame()
        {
            var game = new Game {Id = 1, Name = "Game 1"};
            var games = new List<Game> {game};
            var playedGame = new DateTime(2016, 1, 1);
            var gamePlayedDate = new GamePlayedDate {GameId = game.Id, PlayedDate = playedGame};
            var gamePlayedDates = new List<GamePlayedDate> {gamePlayedDate};

            var context = _fixture.Context
                .GamesContain(games)
                .GamePlayedDatesContain(gamePlayedDates);
            var gameRepository = new GameRepository(context);

            gameRepository.RemovePlayedDate(game.Id, playedGame);

            var result = gameRepository.GetAllPlayedDatesBy(game.Id);

            Assert.False(result.Any());
        }

        [Fact]
        public void DoesNotAddDuplicateDatePlayed()
        {
            var game = new Game {Id = 1, Name = "Game 1"};
            var games = new List<Game> {game};
            var playedDate = new DateTime(2016, 1, 1);

            var context = _fixture.Context
                .GamesContain(games);
            var gameRepository = new GameRepository(context);

            gameRepository.AddPlayedDate(game.Id, playedDate);
            Assert.Throws<ArgumentException>(() => gameRepository.AddPlayedDate(game.Id, playedDate));
        }
    }
}