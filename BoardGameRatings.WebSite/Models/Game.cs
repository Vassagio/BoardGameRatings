using System.Collections.Generic;

namespace BoardGameRatings.WebSite.Models
{
    public class Game : IEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<PlayerGame> Players { get; set; }
        public List<GameCategory> Categories { get; set; }

        public List<GamePlayedDate> PlayedDates { get; set; }
        public int Id { get; set; }
    }
}