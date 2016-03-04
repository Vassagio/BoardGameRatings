using System.Collections.Generic;

namespace BoardGameRatings.WebSite.Models.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity Add(TEntity player);
        void Remove(TEntity entity);
        TEntity GetBy(int entityId);
        void Update(TEntity entity);
    }
}