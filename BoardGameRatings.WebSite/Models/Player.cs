using System.Collections.Generic;

namespace BoardGameRatings.WebSite.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<PlayerGame> Games { get; set; }
    }
}