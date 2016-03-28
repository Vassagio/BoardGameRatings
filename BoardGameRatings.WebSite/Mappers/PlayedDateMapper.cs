using System;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.ViewModels;

namespace BoardGameRatings.WebSite.Mappers
{
    public class PlayedDateMapper : IPlayedDateMapper
    {
        public GamePlayedDate Map(PlayedDateViewModel viewModel)
        {
            return new GamePlayedDate
            {
                Id = viewModel.Id,
                PlayedDate = DateTime.Parse(viewModel.FormattedPlayedDate)
            };
        }

        public PlayedDateViewModel Map(GamePlayedDate playedDate)
        {
            return new PlayedDateViewModel
            {
                Id = playedDate.Id,
                FormattedPlayedDate = playedDate.PlayedDate.ToString("M/d/yyyy")
            };
        }
    }
}