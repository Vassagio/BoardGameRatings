namespace BoardGameRatings.WebSite.Models
{
    public class GameCategory : IEntity
    {
        public int CategoryId { get; set; }
        public int GameId { get; set; }

        public Category Category { get; set; }
        public Game Game { get; set; }
        public int Id { get; set; }
    }
}