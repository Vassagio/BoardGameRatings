using System.Collections.Generic;
using Microsoft.AspNet.Mvc.Rendering;

namespace BoardGameRatings.WebSite.ViewModels
{
    public class PlayerViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public int GameId { get; set; }
        public IEnumerable<SelectListItem> Games { get; set; }
        public IEnumerable<GameViewModel> GamesOwned { get; set; }
    }
}