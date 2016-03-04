using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGameRatings.WebSite.Models
{
    public class PlayerGame
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int GameId { get; set; }

        public Player Player { get; set; }
        public Game Game { get; set; }
    }
}
