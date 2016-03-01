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

        public Player Add(Player player)
        {
            return _mock.Object.Add(player);
        }

        public void Remove(Player player)
        {
            _mock.Object.Remove(player);
        }

        public Player GetById(int id)
        {
            return _mock.Object.GetById(id);
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

        public void VerifyGetAllCalled()
        {
            _mock.Verify(m => m.GetAll(), Times.Once);
        }

        public MockPlayerRepository StubGetByIdToReturn(Player player)
        {
            _mock.Setup(m => m.GetById(It.IsAny<int>())).Returns(player);
            return this;
        }

        public void VerifyGetByIdCalledWith(int id)
        {
            _mock.Verify(m => m.GetById(id), Times.Once);
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
    }
}