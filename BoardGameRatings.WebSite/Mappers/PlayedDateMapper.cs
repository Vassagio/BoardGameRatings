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
                PlayedDate = viewModel.PlayedDate
            };
        }

        public PlayedDateViewModel Map(GamePlayedDate playedDate)
        {
            return new PlayedDateViewModel
            {
                Id = playedDate.Id,
                PlayedDate = playedDate.PlayedDate,
                FormattedPlayedDate = playedDate.PlayedDate.ToString("M/d/yyyy")
            };
        }
    }
}