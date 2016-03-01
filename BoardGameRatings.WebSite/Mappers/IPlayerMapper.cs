using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.ViewModels;

namespace BoardGameRatings.WebSite.Mappers
{
    public interface IPlayerMapper
    {
        Player Map(PlayerViewModel viewModel);
        PlayerViewModel Map(Player player);
    }
}