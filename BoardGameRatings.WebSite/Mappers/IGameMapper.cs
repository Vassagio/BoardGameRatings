using System.Collections.Generic;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.ViewModels;
using Microsoft.AspNet.Mvc.Rendering;

namespace BoardGameRatings.WebSite.Mappers
{
    public interface IGameMapper : IMapper<Game, GameViewModel>, ISelectListItemMapper<Game>
    {
        GameViewModel Map(Game game, IEnumerable<SelectListItem> categories,
            IEnumerable<CategoryViewModel> electedCategories);
    }
}