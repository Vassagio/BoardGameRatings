using System;
using BoardGameRatings.WebSite.Mappers;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.ViewModels;
using Xunit;

namespace BoardGameRatings.WebSite.Tests.Mappers
{
    public class PlayedDateMapperTest
    {
        [Fact]
        public void MapPlayedDateViewModelToGamePlayedDate()
        {
            var viewModel = new PlayedDateViewModel
            {
                Id = 2,
                PlayedDate = new DateTime(2016, 1, 1)
            };
            var mapper = new PlayedDateMapper();
            var playedDate = mapper.Map(viewModel);

            Assert.Equal(viewModel.Id, playedDate.Id);
            Assert.Equal(new DateTime(2016, 1, 1), playedDate.PlayedDate);
        }

        [Fact]
        public void MapGamePlayedDateToPlayedDateViewModel()
        {
            var playedDate = new GamePlayedDate
            {
                Id = 2,
                PlayedDate = new DateTime(2016, 1, 1)
            };
            var mapper = new PlayedDateMapper();
            var viewModel = mapper.Map(playedDate);

            Assert.Equal(playedDate.Id, viewModel.Id);
            Assert.Equal("1/1/2016", viewModel.FormattedPlayedDate);
            Assert.Equal(new DateTime(2016, 1, 1), viewModel.PlayedDate);
        }
    }
}