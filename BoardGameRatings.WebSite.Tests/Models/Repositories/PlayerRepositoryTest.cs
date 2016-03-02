using System;
using System.Collections.Generic;
using System.Linq;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.Models.Repositories;
using BoardGameRatings.WebSite.Tests.Extensions;
using Xunit;

namespace BoardGameRatings.WebSite.Tests.Models.Repositories
{
    public class PlayerRepositoryTest : IClassFixture<RepositoryFixture>, IDisposable
    {
        private readonly RepositoryFixture _fixture;

        public PlayerRepositoryTest(RepositoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.Begin();
        }


        public void Dispose()
        {
            _fixture.End();
        }

        [Fact]
        public void CreateANewPlayerRepository()
        {
            var playerRepository = new PlayerRepository(_fixture.Context);
            Assert.NotNull(playerRepository);
        }

        [Fact]
        public void GetAllPlayers()
        {
            var players = new List<Player>
            {
                new Player {FirstName = "First 1", LastName = "Last 1"},
                new Player {FirstName = "First 2", LastName = "Last 2"},
                new Player {FirstName = "First 3", LastName = "Last 3"}
            };

            var playerRepository = new PlayerRepository(_fixture.Context.PlayersContain(players));

            var result = playerRepository.GetAll().ToList();

            Assert.Equal(3, result.Count());
            Assert.Equal(players, result.OrderBy(r => r.Id));
        }

        [Fact]
        public void AddPlayer()
        {
            var player = new Player
            {
                FirstName = "First 1",
                LastName = "Last 1"
            };

            var playerRepository = new PlayerRepository(_fixture.Context);

            var result = playerRepository.Add(player);

            Assert.NotNull(result);
            Assert.Equal(player, result);
        }

        [Fact]
        public void RemovePlayer()
        {
            var player1 = new Player {FirstName = "First 1", LastName = "Last 1"};
            var player2 = new Player {FirstName = "First 2", LastName = "Last 2"};
            var player3 = new Player {FirstName = "First 3", LastName = "Last 3"};
            var players = new List<Player> {player1, player2, player3};

            var playerRepository = new PlayerRepository(_fixture.Context.PlayersContain(players));

            playerRepository.Remove(player2);
            var result = playerRepository.GetAll().ToList();

            Assert.Equal(2, result.Count());
            Assert.Equal(new List<Player> {player1, player3}, result.OrderBy(r => r.Id));
        }

        [Fact]
        public void GetPlayerById()
        {
            var player3 = new Player {FirstName = "First 3", LastName = "Last 3"};
            var players = new List<Player>
            {
                new Player {FirstName = "First 1", LastName = "Last 1"},
                new Player {FirstName = "First 2", LastName = "Last 2"},
                player3
            };

            var playerRepository = new PlayerRepository(_fixture.Context.PlayersContain(players));

            var result = playerRepository.GetBy(player3.Id);

            Assert.Equal(player3.Id, result.Id);
            Assert.Equal(player3.FirstName, result.FirstName);
            Assert.Equal(player3.LastName, result.LastName);
        }

        [Fact]
        public void UpdatePlayer()
        {
            var players = new List<Player>
            {
                new Player {FirstName = "First 1", LastName = "Last 1"},
                new Player {FirstName = "First 2", LastName = "Last 2"},
                new Player {FirstName = "First 3", LastName = "Last 3"}
            };

            var playerRepository = new PlayerRepository(_fixture.Context.PlayersContain(players));

            var player = playerRepository.GetBy(1);
            player.FirstName = "William";
            player.LastName = "Chronowski";
            playerRepository.Update(player);
            var result = playerRepository.GetBy(1);

            Assert.Equal(1, result.Id);
            Assert.Equal("William", player.FirstName);
            Assert.Equal("Chronowski", player.LastName);
        }

        [Fact]
        public void GetAllGamesOwnedByPlayer()
        {
            var players = new List<Player>
           {
                new Player {Id = 1, FirstName = "First 1", LastName = "Last 1"},
                new Player {Id = 2, FirstName = "First 2", LastName = "Last 2"}
            };

            var games = new List<Game>
            {
               new Game {Id = 1, Name = "Game 1"},
               new Game {Id = 2, Name = "Game 2"},
               new Game {Id = 3, Name = "Game 3"}
            };

            var playerGames = new List<PlayerGame>
            {
                new PlayerGame {GameId = 1, PlayerId = 1},
                new PlayerGame {GameId = 2, PlayerId = 1},
                new PlayerGame {GameId = 3, PlayerId = 1},
                new PlayerGame {GameId = 2, PlayerId = 2},
            };

            var context = _fixture.Context.PlayersContain(players)
                .GamesContain(games)
                .PlayerGamesContain(playerGames);
            var playerRepository = new PlayerRepository(context);

            var resultPlayer1 = playerRepository.GetAllGamesBy(1);
            var resultPlayer2 = playerRepository.GetAllGamesBy(2);

            Assert.Equal(3, resultPlayer1.Count());
            Assert.Equal(1, resultPlayer2.Count());
        }
    }
}