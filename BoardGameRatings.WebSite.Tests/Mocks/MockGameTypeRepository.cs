using System.Collections.Generic;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.Models.Repositories;
using Moq;

namespace BoardGameRatings.WebSite.Tests.Mocks
{
    public class MockGameTypeRepository : IGameTypeRepository
    {
        private readonly Mock<IGameTypeRepository> _mock;

        public MockGameTypeRepository()
        {
            _mock = new Mock<IGameTypeRepository>();
        }

        public IEnumerable<GameType> GetAll()
        {
            return _mock.Object.GetAll();
        }

        public GameType Add(GameType gameType)
        {
            return _mock.Object.Add(gameType);
        }

        public void Remove(GameType gameType)
        {
            _mock.Object.Remove(gameType);
        }

        public GameType GetBy(int gameTypeId)
        {
            return _mock.Object.GetBy(gameTypeId);
        }

        public void Update(GameType gameType)
        {
            _mock.Object.Update(gameType);
        }

        public GameType GetBy(string description)
        {
            return _mock.Object.GetBy(description);
        }

        public MockGameTypeRepository StubGetAllToReturn(IEnumerable<GameType> gameTypes)
        {
            _mock.Setup(m => m.GetAll()).Returns(gameTypes);
            return this;
        }

        public MockGameTypeRepository StubGetByIdToReturn(GameType gameType)
        {
            _mock.Setup(m => m.GetBy(It.IsAny<int>())).Returns(gameType);
            return this;
        }

        public void VerifyGetByCalledWith(int id)
        {
            _mock.Verify(m => m.GetBy(id));
        }

        public void VerifyUpdateCalledWith(GameType gameType)
        {
            _mock.Verify(m => m.Update(gameType));
        }

        public void VerifyAddCalledWith(GameType gameType)
        {
            _mock.Verify(m => m.Add(gameType));
        }

        public void VerifyGetAllCalled()
        {
            _mock.Verify(m => m.GetAll());
        }

        public void VerifyRemoveCalledWith(GameType gameType)
        {
            _mock.Verify(m => m.Remove(gameType));
        }
    }
}