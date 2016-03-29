using System;

namespace BoardGameRatings.WebSite.ViewModels
{
    public class PlayedDateViewModel : IViewModel
    {
        public string FormattedPlayedDate { get; set; }
        public DateTime PlayedDate { get; set; }
        public int Id { get; set; }
    }
}