using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.ViewModels;

namespace BoardGameRatings.WebSite.Mappers
{
    public interface IGameMapper
    {
        Game Map(GameViewModel viewModel);
        GameViewModel Map(Game game);
    }
}