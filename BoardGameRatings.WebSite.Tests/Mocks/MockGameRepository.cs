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

        public Game Add(Game player)
        {
            return _mock.Object.Add(player);
        }

        public void Remove(Game game)
        {
            _mock.Object.Remove(game);
        }

        public Game GetBy(int gameId)
        {
            return _mock.Object.GetBy(gameId);
        }

        public void Update(Game game)
        {
            _mock.Object.Update(game);
        }

        public Game GetBy(string name)
        {
            return _mock.Object.GetBy(name);
        }

        public void AddElectedCategory(int gameId, int categoryId)
        {
            _mock.Object.AddElectedCategory(gameId, categoryId);
        }

        public IEnumerable<Category> GetAllCategoriesBy(int gameId)
        {
            return _mock.Object.GetAllCategoriesBy(gameId);
        }

        public GameCategory GetGameCategoryBy(int gameId, int categoryId)
        {
            return _mock.Object.GetGameCategoryBy(gameId, categoryId);
        }

        public MockGameRepository StubGetAllToReturn(List<Game> games)
        {
            _mock.Setup(m => m.GetAll()).Returns(games);
            return this;
        }

        public MockGameRepository StubGetByToReturn(Game game)
        {
            _mock.Setup(m => m.GetBy(It.IsAny<int>())).Returns(game);
            return this;
        }

        public void VerifyGetAllCalled()
        {
            _mock.Verify(m => m.GetAll());
        }

        public void VerifyRemoveCalledWith(Game game)
        {
            _mock.Verify(m => m.Remove(game));
        }

        public void VerifyUpdateCalledWith(Game game)
        {
            _mock.Verify(m => m.Update(game));
        }

        public void VerifyGetByCalledWith(int id)
        {
            _mock.Verify(m => m.GetBy(id));
        }

        public void VerifyAddCalledWith(Game game)
        {
            _mock.Verify(m => m.Add(game));
        }

        public MockGameRepository StubGetByNameToReturn(Game game)
        {
            _mock.Setup(m => m.GetBy(It.IsAny<string>())).Returns(game);
            return this;
        }

        internal void VerifyGetByCalledWith(string name)
        {
            _mock.Verify(m => m.GetBy(name));
        }

        public MockGameRepository StubGetAllCategoriesByToReturn(IEnumerable<Category> categories)
        {
            _mock.Setup(m => m.GetAllCategoriesBy(It.IsAny<int>())).Returns(categories);
            return this;
        }

        public void VerifyGetAllCategoriesByCalledWith(int gameId)
        {
            _mock.Verify(m => m.GetAllCategoriesBy(gameId));
        }
    }
}