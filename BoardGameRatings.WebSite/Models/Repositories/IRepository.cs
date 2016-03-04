using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGameRatings.WebSite.Models.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity Add(TEntity entity);
        void Remove(TEntity entity);
        TEntity GetBy(int entityId);
        void Update(TEntity entity);
    }
}
