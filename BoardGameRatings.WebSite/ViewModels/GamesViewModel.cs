using System.Collections.Generic;

namespace BoardGameRatings.WebSite.ViewModels
{
    public class GamesViewModel
    {
        public IEnumerable<GameViewModel> Games { get; set; }
    }
}