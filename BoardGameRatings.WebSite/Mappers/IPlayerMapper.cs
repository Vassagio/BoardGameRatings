using System.Collections.Generic;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.ViewModels;
using Microsoft.AspNet.Mvc.Rendering;

namespace BoardGameRatings.WebSite.Mappers
{
    public interface IPlayerMapper : IMapper<Player, PlayerViewModel>
    {
        PlayerViewModel Map(Player player, IEnumerable<SelectListItem> games, IEnumerable<GameViewModel> gamesOwned);
    }
}