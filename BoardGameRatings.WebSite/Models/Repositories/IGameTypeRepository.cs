namespace BoardGameRatings.WebSite.Models.Repositories
{
    public interface IGameTypeRepository : IRepository<GameType>
    {
        GameType GetBy(string description);
    }
}