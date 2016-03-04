using System.Collections.Generic;

namespace BoardGameRatings.WebSite.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<GameCategory> Games { get; set; }
    }
}