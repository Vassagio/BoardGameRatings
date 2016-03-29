using System.Linq;
using BoardGameRatings.WebSite.Mappers;
using BoardGameRatings.WebSite.Models.Repositories;
using BoardGameRatings.WebSite.ViewModels;

namespace BoardGameRatings.WebSite.Contexts
{
    public class PlayersContext : IPlayersContext
    {
        private readonly IPlayerMapper _mapper;
        private readonly IPlayerRepository _playerRepository;

        public PlayersContext(IPlayerRepository playerRepository, IPlayerMapper mapper)
        {
            _mapper = mapper;
            _playerRepository = playerRepository;
        }

        public PlayersViewModel BuildViewModel()
        {
            var players = _playerRepository.GetAll()
                .Select(player => _mapper.Map(player));
            return new PlayersViewModel
            {
                Players = players
            };
        }

        public void Remove(int id)
        {
            var player = _playerRepository.GetBy(id);
            _playerRepository.Remove(player);
        }
    }
}