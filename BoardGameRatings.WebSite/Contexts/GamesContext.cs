using System.Linq;
using BoardGameRatings.WebSite.Models.Repositories;
using BoardGameRatings.WebSite.ViewModels;

namespace BoardGameRatings.WebSite.Contexts
{
    public class GamesContext : IGamesContext
    {
        private readonly IGameRepository _gameRepository;

        public GamesContext(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public GamesViewModel BuildViewModel()
        {
            var games = _gameRepository.GetAll().Select(game => new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description
            });

            return new GamesViewModel
            {
                Games = games
            };
        }
    }
}