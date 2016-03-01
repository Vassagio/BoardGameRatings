using System.Collections.Generic;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.Models.Repositories;
using Moq;

namespace BoardGameRatings.WebSite.Tests.Mocks
{
    public class MockGameRepository : IGameRepository
    {
        private readonly Mock<IGameRepository> _mock;

        public MockGameRepository()
        {
            _mock = new Mock<IGameRepository>();
        }

        public IEnumerable<Game> GetAll()
        {
            return _mock.Object.GetAll();
        }

        public Game Add(Game game)
        {
            return _mock.Object.Add(game);
        }

        public void Remove(Game game)
        {
            _mock.Object.Remove(game);
        }

        public Game GetById(int id)
        {
            return _mock.Object.GetById(id);
        }

        public void Update(Game game)
        {
            _mock.Object.Update(game);
        }

        public MockGameRepository StubGetAllToReturn(List<Game> games)
        {
            _mock.Setup(m => m.GetAll()).Returns(games);
            return this;
        }

        public MockGameRepository StubGetByIdToReturn(Game game)
        {
            _mock.Setup(m => m.GetById(It.IsAny<int>())).Returns(game);
            return this;
        }

        public void VerifyGetAllCalled()
        {
            _mock.Verify(m => m.GetAll(), Times.Once);
        }

        public void VerifyRemoveCalledWith(Game game)
        {
            _mock.Verify(m => m.Remove(game), Times.Once);
        }

        public void VerifyUpdateCalledWith(Game game)
        {
            _mock.Verify(m => m.Update(game), Times.Once);
        }

        public void VerifyGetByIdCalledWith(int id)
        {
            _mock.Verify(m => m.GetById(id), Times.Once);
        }

        public void VerifyAddCalledWith(Game game)
        {
            _mock.Verify(m => m.Add(game), Times.Once);
        }
    }
}