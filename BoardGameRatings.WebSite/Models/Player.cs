using System.Collections.Generic;

namespace BoardGameRatings.WebSite.Models
{
    public class Player : IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<PlayerGame> Games { get; set; }
        public int Id { get; set; }
    }
}