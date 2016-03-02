using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.ViewModels;
using Microsoft.AspNet.Mvc.Rendering;

namespace BoardGameRatings.WebSite.Mappers
{
    public interface IGameMapper
    {
        Game Map(GameViewModel viewModel);
        GameViewModel Map(Game game);
        SelectListItem SelectMap(Game game);
    }
}