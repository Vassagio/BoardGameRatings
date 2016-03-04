using System.Collections.Generic;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.Models.Repositories;
using Moq;

namespace BoardGameRatings.WebSite.Tests.Mocks
{
    public class MockCategoryRepository : ICategoryRepository
    {
        private readonly Mock<ICategoryRepository> _mock;

        public MockCategoryRepository()
        {
            _mock = new Mock<ICategoryRepository>();
        }

        public IEnumerable<Category> GetAll()
        {
            return _mock.Object.GetAll();
        }

        public Category Add(Category player)
        {
            return _mock.Object.Add(player);
        }

        public void Remove(Category category)
        {
            _mock.Object.Remove(category);
        }

        public Category GetBy(int categoryId)
        {
            return _mock.Object.GetBy(categoryId);
        }

        public void Update(Category category)
        {
            _mock.Object.Update(category);
        }

        public Category GetBy(string description)
        {
            return _mock.Object.GetBy(description);
        }

        public MockCategoryRepository StubGetAllToReturn(IEnumerable<Category> categories)
        {
            _mock.Setup(m => m.GetAll()).Returns(categories);
            return this;
        }

        public MockCategoryRepository StubGetByIdToReturn(Category category)
        {
            _mock.Setup(m => m.GetBy(It.IsAny<int>())).Returns(category);
            return this;
        }

        public void VerifyGetByCalledWith(int id)
        {
            _mock.Verify(m => m.GetBy(id));
        }

        public void VerifyUpdateCalledWith(Category category)
        {
            _mock.Verify(m => m.Update(category));
        }

        public void VerifyAddCalledWith(Category category)
        {
            _mock.Verify(m => m.Add(category));
        }

        public void VerifyGetAllCalled()
        {
            _mock.Verify(m => m.GetAll());
        }

        public void VerifyRemoveCalledWith(Category category)
        {
            _mock.Verify(m => m.Remove(category));
        }
    }
}