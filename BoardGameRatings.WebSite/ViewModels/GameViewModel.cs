using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc.Rendering;

namespace BoardGameRatings.WebSite.ViewModels
{
    public class GameViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
        public IEnumerable<CategoryViewModel> ElectedCategories { get; set; } = new List<CategoryViewModel>();
        public DateTime SelectedPlayedDate { get; set; }
        public IEnumerable<PlayedDateViewModel> PlayedDates { get; set; } = new List<PlayedDateViewModel>();
    }
}