﻿using BoardGameRatings.WebSite.Mappers;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.ViewModels;
using Moq;

namespace BoardGameRatings.WebSite.Tests.Mocks
{
    public class MockPlayedDateMapper : IPlayedDateMapper
    {
        private readonly Mock<IPlayedDateMapper> _mock;

        public MockPlayedDateMapper()
        {
            _mock = new Mock<IPlayedDateMapper>();
        }

        public GamePlayedDate Map(PlayedDateViewModel viewModel)
        {
            return _mock.Object.Map(viewModel);
        }

        public PlayedDateViewModel Map(GamePlayedDate gamePlayedDate)
        {
            return _mock.Object.Map(gamePlayedDate);
        }

        public MockPlayedDateMapper StubMapToReturn(PlayedDateViewModel playedDateViewModel)
        {
            _mock.Setup(m => m.Map(It.IsAny<GamePlayedDate>()))
                .Returns(playedDateViewModel);
            return this;
        }

        public void VerifyMapCalledWith(GamePlayedDate gamePlayedDate)
        {
            _mock.Verify(m => m.Map(gamePlayedDate));
        }
    }
}