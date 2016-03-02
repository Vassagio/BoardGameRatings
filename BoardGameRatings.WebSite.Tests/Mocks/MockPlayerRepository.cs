using System.Collections.Generic;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.Models.Repositories;
using Moq;

namespace BoardGameRatings.WebSite.Tests.Mocks
{
    public class MockPlayerRepository : IPlayerRepository
    {
        private readonly Mock<IPlayerRepository> _mock;

        public MockPlayerRepository()
        {
            _mock = new Mock<IPlayerRepository>();
        }

        public IEnumerable<Player> GetAll()
        {
            return _mock.Object.GetAll();
        }

        public IEnumerable<Game> GetAllGamesBy(int playerId)
        {
            return _mock.Object.GetAllGamesBy(playerId);
        }

        public Player Add(Player player)
        {
            return _mock.Object.Add(player);
        }

        public void Remove(Player player)
        {
            _mock.Object.Remove(player);
        }

        public Player GetBy(int playerId)
        {
            return _mock.Object.GetBy(playerId);
        }

        public void Update(Player player)
        {
            _mock.Object.Update(player);
        }

        public MockPlayerRepository StubGetAllToReturn(IEnumerable<Player> players)
        {
            _mock.Setup(m => m.GetAll()).Returns(players);
            return this;
        }

        public MockPlayerRepository StubGetAllGamesByToReturn(IEnumerable<Game> games)
        {
            _mock.Setup(m => m.GetAllGamesBy(It.IsAny<int>())).Returns(games);
            return this;
        }

        public void VerifyGetAllCalled()
        {
            _mock.Verify(m => m.GetAll(), Times.Once);
        }

        public MockPlayerRepository StubGetByToReturn(Player player)
        {
            _mock.Setup(m => m.GetBy(It.IsAny<int>())).Returns(player);
            return this;
        }

        public void VerifyGetByCalledWith(int playerId)
        {
            _mock.Verify(m => m.GetBy(playerId), Times.Once);
        }

        public void VerifyRemoveCalledWith(Player player)
        {
            _mock.Verify(m => m.Remove(player), Times.Once);
        }

        public void VerifyUpdateCalledWith(Player player)
        {
            _mock.Verify(m => m.Update(player), Times.Once);
        }

        public void VerifyAddCalledWith(Player player)
        {
            _mock.Verify(m => m.Add(player), Times.Once);
        }

        public void VerifyGetAllGamesByCalledWith(int playerId)
        {
            _mock.Verify(m => m.GetAllGamesBy(playerId));
        }
    }
}