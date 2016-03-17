using System.Collections.Generic;
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
                Description = game.Description,
                Categories = new List<SelectListItem>()
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

        public GameViewModel Map(Game game, IEnumerable<SelectListItem> categories,
            IEnumerable<CategoryViewModel> electedCategories)
        {
            var viewModel = new GameViewModel();
            if (game != null)
                viewModel = Map(game);
            viewModel.Categories = categories;
            viewModel.ElectedCategories = electedCategories;
            return viewModel;
        }
    }
}