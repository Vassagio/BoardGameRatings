using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.ViewModels;
using Microsoft.AspNet.Mvc.Rendering;

namespace BoardGameRatings.WebSite.Mappers
{
    public class GameMapper : IGameMapper
    {
        public Game Map(GameViewModel viewModel)
        {
            return new Game
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Description = viewModel.Description
            };
        }

        public GameViewModel Map(Game game)
        {
            return new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description
            };
        }

        public SelectListItem SelectMap(Game game)
        {
            return new SelectListItem
            {
                Value = game.Id.ToString(),
                Text = game.Name
            };
        }
    }
}