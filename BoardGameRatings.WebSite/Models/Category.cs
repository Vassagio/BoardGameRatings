using System.Collections.Generic;

namespace BoardGameRatings.WebSite.Models
{
    public class Category : IEntity
    {
        public string Description { get; set; }
        public List<GameCategory> Games { get; set; }
        public int Id { get; set; }
    }
}