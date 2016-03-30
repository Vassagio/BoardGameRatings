namespace BoardGameRatings.WebSite.Models
{
    public class PlayerGame : IEntity
    {
        public int PlayerId { get; set; }
        public int GameId { get; set; }

        public Player Player { get; set; }
        public Game Game { get; set; }
        public int Id { get; set; }
    }
}