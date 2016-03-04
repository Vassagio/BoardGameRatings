using System;
using System.Collections.Generic;
using System.Linq;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.Models.Repositories;
using BoardGameRatings.WebSite.Tests.Extensions;
using Xunit;

namespace BoardGameRatings.WebSite.Tests.Models.Repositories
{
    public class GameTypeRepositoryTest : IClassFixture<RepositoryFixture>, IDisposable
    {
        private readonly RepositoryFixture _fixture;

        public GameTypeRepositoryTest(RepositoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.Begin();
        }


        public void Dispose()
        {
            _fixture.End();
        }

        [Fact]
        public void CreateANewGameTypeRepository()
        {
            var gameTypeRepository = new GameTypeRepository(_fixture.Context);
            Assert.NotNull(gameTypeRepository);
        }

        [Fact]
        public void GetAllGameTypes()
        {
            var gameTypes = new List<GameType>
            {
                new GameType {Description = "GameType 1"},
                new GameType {Description = "GameType 2"},
                new GameType {Description = "GameType 3"}
            };

            var gameTypeRepository = new GameTypeRepository(_fixture.Context.GameTypesContain(gameTypes));

            var result = gameTypeRepository.GetAll().ToList();

            Assert.Equal(3, result.Count());
            Assert.Equal(gameTypes, result.OrderBy(r => r.Id));
        }

        [Fact]
        public void AddGameType()
        {
            var gameType = new GameType
            {
                Description = "GameType 1"
            };

            var gameTypeRepository = new GameTypeRepository(_fixture.Context);

            var result = gameTypeRepository.Add(gameType);

            Assert.NotNull(result);
            Assert.Equal(gameType, result);
        }

        [Fact]
        public void DoesNotAddDuplicateGameType()
        {
            var gameType = new GameType
            {
                Description = "GameType 1"
            };

            var gameTypeRepository = new GameTypeRepository(_fixture.Context);

            gameTypeRepository.Add(gameType);
            gameTypeRepository.Add(gameType);

            var result = gameTypeRepository.GetAll();

            Assert.Equal(1, result.Count());
            Assert.Equal(gameType, result.First());
        }

        [Fact]
        public void RemoveGameType()
        {
            var gameType1 = new GameType {Description = "GameType 1"};
            var gameType2 = new GameType {Description = "GameType 2"};
            var gameType3 = new GameType {Description = "GameType 3"};
            var gameTypes = new List<GameType> {gameType1, gameType2, gameType3};

            var gameTypeRepository = new GameTypeRepository(_fixture.Context.GameTypesContain(gameTypes));

            gameTypeRepository.Remove(gameType2);
            var result = gameTypeRepository.GetAll().ToList();

            Assert.Equal(2, result.Count());
            Assert.Equal(new List<GameType> {gameType1, gameType3}, result.OrderBy(r => r.Id));
        }

        [Fact]
        public void GetGameTypeById()
        {
            var gameType3 = new GameType {Description = "GameType 3"};
            var gameTypes = new List<GameType>
            {
                new GameType {Description = "GameType 1"},
                new GameType {Description = "GameType 2"},
                gameType3
            };

            var gameTypeRepository = new GameTypeRepository(_fixture.Context.GameTypesContain(gameTypes));

            var result = gameTypeRepository.GetBy(gameType3.Id);

            Assert.Equal(gameType3.Id, result.Id);
            Assert.Equal(gameType3.Description, result.Description);
        }

        [Theory]
        [InlineData("GameType 3")]
        [InlineData("gameType 3")]
        public void GetGameTypeByName(string description)
        {
            var gameType3 = new GameType {Description = description};
            var gameTypes = new List<GameType>
            {
                new GameType {Description = "GameType 1"},
                new GameType {Description = "GameType 2"},
                gameType3
            };

            var gameTypeRepository = new GameTypeRepository(_fixture.Context.GameTypesContain(gameTypes));

            var result = gameTypeRepository.GetBy(gameType3.Description);

            Assert.Equal(gameType3.Id, result.Id);
            Assert.Equal(gameType3.Description, result.Description);
        }

        [Fact]
        public void UpdateGameType()
        {
            var gameTypes = new List<GameType>
            {
                new GameType {Description = "GameType 1"},
                new GameType {Description = "GameType 2"},
                new GameType {Description = "GameType 3"}
            };

            var gameTypeRepository = new GameTypeRepository(_fixture.Context.GameTypesContain(gameTypes));

            var gameType = gameTypeRepository.GetBy(1);
            gameType.Description = "Campaign";
            gameTypeRepository.Update(gameType);
            var result = gameTypeRepository.GetBy(1);

            Assert.Equal(1, result.Id);
            Assert.Equal("Campaign", gameType.Description);
        }
    }
}