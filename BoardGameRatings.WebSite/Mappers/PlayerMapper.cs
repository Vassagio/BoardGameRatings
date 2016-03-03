using System.Collections.Generic;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.ViewModels;
using Microsoft.AspNet.Mvc.Rendering;

namespace BoardGameRatings.WebSite.Mappers
{
    public class PlayerMapper : IPlayerMapper
    {
        public Player Map(PlayerViewModel viewModel)
        {
            return new Player
            {
                Id = viewModel.Id,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName
            };
        }

        public PlayerViewModel Map(Player player)
        {
            return new PlayerViewModel
            {
                Id = player.Id,
                FirstName = player.FirstName,
                LastName = player.LastName,
                Games = new List<SelectListItem>()
            };
        }

        public PlayerViewModel Map(Player player, IEnumerable<SelectListItem> games, IEnumerable<GameViewModel> gamesOwned)
        {
            var viewModel = new PlayerViewModel();
            if (player != null)
                viewModel = Map(player);
            viewModel.Games = games;
            viewModel.GamesOwned = gamesOwned;
            return viewModel;
        }
    }
}