using System.Linq;
using BoardGameRatings.WebSite.Mappers;
using BoardGameRatings.WebSite.Models.Repositories;
using BoardGameRatings.WebSite.ViewModels;

namespace BoardGameRatings.WebSite.Contexts
{
    public class GamesContext : IGamesContext
    {
        private readonly IGameRepository _gameRepository;
        private readonly IGameMapper _mapper;

        public GamesContext(IGameRepository gameRepository, IGameMapper mapper)
        {
            _mapper = mapper;
            _gameRepository = gameRepository;
        }

        public GamesViewModel BuildViewModel()
        {
            var games = _gameRepository.GetAll().Select(game => _mapper.Map(game));
            return new GamesViewModel
            {
                Games = games
            };
        }

        public void Remove(int id)
        {
            var game = _gameRepository.GetById(id);
            _gameRepository.Remove(game);
        }
    }
}