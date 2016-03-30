using System.Collections.Generic;
using BoardGameRatings.WebSite.Mappers;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.ViewModels;
using Microsoft.AspNet.Mvc.Rendering;
using Moq;

namespace BoardGameRatings.WebSite.Tests.Mocks
{
    public class MockGameMapper : IGameMapper
    {
        private readonly Mock<IGameMapper> _mock;

        public MockGameMapper()
        {
            _mock = new Mock<IGameMapper>();
        }

        public Game Map(GameViewModel viewModel)
        {
            return _mock.Object.Map(viewModel);
        }

        public GameViewModel Map(Game game)
        {
            return _mock.Object.Map(game);
        }

        public SelectListItem SelectMap(Game game)
        {
            return _mock.Object.SelectMap(game);
        }

        public GameViewModel Map(Game game, IEnumerable<SelectListItem> categories,
            IEnumerable<CategoryViewModel> electedCategories, IEnumerable<PlayedDateViewModel> playedDates)
        {
            return _mock.Object.Map(game, categories, electedCategories, playedDates);
        }

        public void VerifyMapCalledWith(Game game)
        {
            _mock.Verify(m => m.Map(game));
        }

        public void VerifyMapCalledWith(GameViewModel gameViewModel)
        {
            _mock.Verify(m => m.Map(gameViewModel));
        }

        public void VerifySelectMapCalledWith(Game game)
        {
            _mock.Verify(m => m.SelectMap(game));
        }

        public MockGameMapper StubMapToReturn(GameViewModel gameViewModel)
        {
            _mock.Setup(m => m.Map(It.IsAny<Game>()))
                .Returns(gameViewModel);
            return this;
        }


        public MockGameMapper StubSelectMapToReturn(SelectListItem item)
        {
            _mock.Setup(m => m.SelectMap(It.IsAny<Game>()))
                .Returns(item);
            return this;
        }

        public MockGameMapper StubMapToReturn(Game game)
        {
            _mock.Setup(m => m.Map(It.IsAny<GameViewModel>()))
                .Returns(game);
            return this;
        }

        public void VerifyMapCalledWith(Game game, IEnumerable<SelectListItem> categorySelectListItems,
            IEnumerable<CategoryViewModel> electedCategories, IEnumerable<PlayedDateViewModel> playedDates)
        {
            _mock.Verify(m => m.Map(game, categorySelectListItems, electedCategories, playedDates));
        }

        public MockGameMapper StubMapWithCategoriesToReturn(GameViewModel gameViewModel)
        {
            _mock.Setup(m => m.Map(It.IsAny<Game>(), It.IsAny<IEnumerable<SelectListItem>>(), It.IsAny<IEnumerable<CategoryViewModel>>(), It.IsAny<IEnumerable<PlayedDateViewModel>>()))
                .Returns(gameViewModel);
            return this;
        }
    }
}