namespace BoardGameRatings.WebSite.Models.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category GetBy(string description);
    }
}