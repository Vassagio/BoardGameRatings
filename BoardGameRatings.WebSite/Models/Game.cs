using System.Collections.Generic;

namespace BoardGameRatings.WebSite.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<PlayerGame> Players { get; set; }        
    }
}