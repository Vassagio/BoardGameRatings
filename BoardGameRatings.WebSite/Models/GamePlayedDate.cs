using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
