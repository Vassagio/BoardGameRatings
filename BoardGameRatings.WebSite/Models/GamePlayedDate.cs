using System;

namespace BoardGameRatings.WebSite.Models
{
    public class GamePlayedDate : IEntity
    {
        public int GameId { get; set; }
        public DateTime PlayedDate { get; set; }

        public Game Game { get; set; }
        public int Id { get; set; }
    }
}