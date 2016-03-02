using System.Collections.Generic;
using BoardGameRatings.WebSite.Models;
using Microsoft.AspNet.Mvc.Rendering;

namespace BoardGameRatings.WebSite.ViewModels
{
    public class PlayerViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int GameId { get; set; }
        public IEnumerable<SelectListItem> Games { get; set; }
    }
}