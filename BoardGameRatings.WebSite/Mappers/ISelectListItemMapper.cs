using BoardGameRatings.WebSite.Models;
using Microsoft.AspNet.Mvc.Rendering;

namespace BoardGameRatings.WebSite.Mappers
{
    public interface ISelectListItemMapper<TEntity> where TEntity : IEntity
    {
        SelectListItem SelectMap(TEntity category);
    }
}