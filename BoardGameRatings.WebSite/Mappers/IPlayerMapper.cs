using System.Collections.Generic;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.ViewModels;
using Microsoft.AspNet.Mvc.Rendering;

namespace BoardGameRatings.WebSite.Mappers
{
    public interface IPlayerMapper
    {
        Player Map(PlayerViewModel viewModel);
        PlayerViewModel Map(Player player);
        PlayerViewModel Map(Player player, IEnumerable<SelectListItem> games, IEnumerable<GameViewModel> gamesOwned);
    }
}