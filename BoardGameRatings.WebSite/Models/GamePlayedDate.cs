using System;

namespace BoardGameRatings.WebSite.Models
{
    public class GamePlayedDate
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public DateTime PlayedDate { get; set; }

        public Game Game { get; set; }
    }
}