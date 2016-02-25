using System;
using System.Collections.Generic;
using System.Linq;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.Models.Repositories;
using BoardGameRatings.WebSite.Tests.Extensions;
using Xunit;

namespace BoardGameRatings.WebSite.Tests.Models.Repositories {
    public class GameRepositoryTest : IClassFixture<RepositoryFixture>, IDisposable {
        private readonly RepositoryFixture _fixture;

        public GameRepositoryTest(RepositoryFixture fixture) {
            _fixture = fixture;
            _fixture.Begin();
        }

        [Fact]
        public void CreateANewGameRepository() {
            var gameRepository = new GameRepository(_fixture.Context);
            Assert.NotNull(gameRepository);
        }

        [Fact,]
        public void GetAllGames() {
            var games = new List<Game>
            {
                new Game {Name = "Game 1"},
                new Game {Name = "Game 2"},
                new Game {Name = "Game 3"}
            };

            var gameRepository = new GameRepository(_fixture.Context.GamesContain(games));

            var result = gameRepository.GetAll().ToList();

            Assert.Equal(3, result.Count());
            Assert.Equal(games, result.OrderBy(r => r.Id));
        }

        [Fact]
        public void AddGame() {
            var game = new Game() {
                Name = "Game 1"
            };

            var gameRepository = new GameRepository(_fixture.Context);

            var result = gameRepository.Add(game);

            Assert.NotNull(result);
            Assert.Equal(game, result);
        }

        [Fact]
        public void RemoveGame() {
            var game1 = new Game { Name = "Game 1" };
            var game2 = new Game { Name = "Game 2" };
            var game3 = new Game { Name = "Game 3" };
            var games = new List<Game> { game1, game2, game3 };

            var gameRepository = new GameRepository(_fixture.Context.GamesContain(games));

            gameRepository.Remove(game2);
            var result = gameRepository.GetAll().ToList();

            Assert.Equal(2, result.Count());
            Assert.Equal(new List<Game> {game1, game3}, result.OrderBy(r => r.Id));
        }

        [Fact]
        public void FindGame()
        {
            var game3 = new Game {Name = "Game 3"};
            var games = new List<Game>
            {
                new Game {Name = "Game 1"},
                new Game {Name = "Game 2"},
                game3
            };

            var gameRepository = new GameRepository(_fixture.Context.GamesContain(games));

            var result = gameRepository.Find(game3.Id);

            Assert.Equal(game3.Id, result.Id);
            Assert.Equal(game3.Name, result.Name);
            Assert.Equal(game3.Description, result.Description);            
        }

        public void Dispose() {
            _fixture.End();
        }
    }
}