using BoardGameRatings.WebSite.Mappers;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.Models.Repositories;
using BoardGameRatings.WebSite.ViewModels;

namespace BoardGameRatings.WebSite.Contexts
{
    public class PlayerContext : IPlayerContext
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IPlayerMapper _mapper;

        public PlayerContext(IPlayerRepository playerRepository, IPlayerMapper mapper)
        {
            _playerRepository = playerRepository;
            _mapper = mapper;
        }


        public PlayerViewModel BuildViewModel(int? id = null)
        {
            if (id.HasValue)
            {
                var player = _playerRepository.GetById(id.Value);
                return _mapper.Map(player);
            }
            return new PlayerViewModel();
        }

        public void Save(PlayerViewModel model)
        {
            var player = _playerRepository.GetById(model.Id);
            if (player != null)
                Update(player, model);
            else
                Add(model);
        }

        private void Update(Player player, PlayerViewModel model)
        {
            player.FirstName = model.FirstName;
            player.LastName = model.LastName;
            _playerRepository.Update(player);
        }

        private void Add(PlayerViewModel model)
        {
            var player = _mapper.Map(model);
            _playerRepository.Add(player);
        }
    }
}