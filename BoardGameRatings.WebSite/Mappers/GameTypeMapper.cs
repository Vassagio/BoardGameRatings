using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.ViewModels;
using Microsoft.AspNet.Mvc.Rendering;

namespace BoardGameRatings.WebSite.Mappers
{
    public class GameTypeMapper : IGameTypeMapper
    {
        public GameType Map(GameTypeViewModel viewModel)
        {
            return new GameType
            {
                Id = viewModel.Id,
                Description = viewModel.Description
            };
        }

        public GameTypeViewModel Map(GameType gameType)
        {
            return new GameTypeViewModel
            {
                Id = gameType.Id,
                Description = gameType.Description
            };
        }

        public SelectListItem SelectMap(GameType gameType)
        {
            return new SelectListItem
            {
                Value = gameType.Id.ToString(),
                Text = gameType.Description
            };
        }
    }
}