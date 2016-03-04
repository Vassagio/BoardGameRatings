using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.ViewModels;
using Microsoft.AspNet.Mvc.Rendering;

namespace BoardGameRatings.WebSite.Mappers
{
    public interface IGameTypeMapper
    {
        GameType Map(GameTypeViewModel viewModel);
        GameTypeViewModel Map(GameType gameType);
        SelectListItem SelectMap(GameType gameType);
    }
}