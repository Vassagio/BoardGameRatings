namespace BoardGameRatings.WebSite.Models.Repositories
{
    public interface IGameRepository : IRepository<Game>
    {
        Game GetBy(string name);
    }
}