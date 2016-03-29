using System;
using BoardGameRatings.WebSite.Contexts;
using BoardGameRatings.WebSite.ViewModels;
using Moq;

namespace BoardGameRatings.WebSite.Tests.Mocks
{
    public class MockGameContext : IGameContext
    {
        private readonly Mock<IGameContext> _mock;

        public MockGameContext()
        {
            _mock = new Mock<IGameContext>();
        }

        public GameViewModel BuildViewModel(int? id = null)
        {
            return _mock.Object.BuildViewModel(id);
        }

        public void Save(GameViewModel model)
        {
            _mock.Object.Save(model);
        }

        public void AddElectedCategory(int gameId, int categoryId)
        {
            _mock.Object.AddElectedCategory(gameId, categoryId);
        }

        public void RemoveElectedCategory(int gameId, int categoryId)
        {
            _mock.Object.RemoveElectedCategory(gameId, categoryId);
        }

        public void AddPlayedDate(int gameId, DateTime playedDate)
        {
            _mock.Object.AddPlayedDate(gameId, playedDate);
        }

        public void RemovePlayedDate(int gameId, DateTime playedDate)
        {
            _mock.Object.RemovePlayedDate(gameId, playedDate);
        }

        public MockGameContext StubBuildViewModelToReturn(GameViewModel gameViewModel)
        {
            _mock.Setup(m => m.BuildViewModel(It.IsAny<int?>()))
                .Returns(gameViewModel);
            return this;
        }

        public void VerifyBuildViewModelCalledWith(int? id = null)
        {
            _mock.Verify(m => m.BuildViewModel(id));
        }

        public void VerifySaveCalledWith(GameViewModel gameViewModel)
        {
            _mock.Verify(m => m.Save(gameViewModel));
        }

        public void VerifyAddElectedCategoryCalledWith(int gameId, int categoryId)
        {
            _mock.Verify(m => m.AddElectedCategory(gameId, categoryId));
        }

        public void VerifyRemoveElectedCategoryCalledWith(int gameId, int categoryId)
        {
            _mock.Verify(m => m.RemoveElectedCategory(gameId, categoryId));
        }

        public void VerifyAddPlayedDateCalledWith(int gameId, DateTime playedDate)
        {
            _mock.Verify(m => m.AddPlayedDate(gameId, playedDate));
        }

        public void VerifyRemovePlayedDateCalledWith(int gameId, DateTime playedDate)
        {
            _mock.Verify(m => m.RemovePlayedDate(gameId, playedDate));
        }
    }
}