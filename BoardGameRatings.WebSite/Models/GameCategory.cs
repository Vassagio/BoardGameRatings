namespace BoardGameRatings.WebSite.Models
{
    public class GameCategory
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int GameId { get; set; }

        public Category Category { get; set; }
        public Game Game { get; set; }
    }
}