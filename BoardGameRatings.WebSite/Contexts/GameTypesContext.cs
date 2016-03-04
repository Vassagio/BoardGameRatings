using System.Linq;
using BoardGameRatings.WebSite.Mappers;
using BoardGameRatings.WebSite.Models.Repositories;
using BoardGameRatings.WebSite.ViewModels;

namespace BoardGameRatings.WebSite.Contexts
{
    public class GameTypesContext : IGameTypesContext
    {
        private readonly IGameTypeRepository _gameTypeRepository;
        private readonly IGameTypeMapper _mapper;

        public GameTypesContext(IGameTypeRepository gameTypeRepository, IGameTypeMapper mapper)
        {
            _mapper = mapper;
            _gameTypeRepository = gameTypeRepository;
        }

        public GameTypesViewModel BuildViewModel()
        {
            var gameTypes = _gameTypeRepository.GetAll().Select(gameType => _mapper.Map(gameType));
            return new GameTypesViewModel
            {
                GameTypes = gameTypes
            };
        }

        public void Remove(int id)
        {
            var gameType = _gameTypeRepository.GetBy(id);
            _gameTypeRepository.Remove(gameType);
        }
    }
}