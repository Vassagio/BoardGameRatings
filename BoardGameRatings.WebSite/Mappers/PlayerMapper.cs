using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.ViewModels;

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
                LastName = player.LastName
            };
        }
    }
}