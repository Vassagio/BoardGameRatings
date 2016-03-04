using BoardGameRatings.WebSite.Mappers;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.Models.Repositories;
using BoardGameRatings.WebSite.ViewModels;

namespace BoardGameRatings.WebSite.Contexts
{
    public class GameTypeContext : IGameTypeContext
    {
        private readonly IGameTypeRepository _gameTypeRepository;
        private readonly IGameTypeMapper _mapper;

        public GameTypeContext(IGameTypeRepository gameTypeRepository, IGameTypeMapper mapper)
        {
            _gameTypeRepository = gameTypeRepository;
            _mapper = mapper;
        }


        public GameTypeViewModel BuildViewModel(int? id = null)
        {
            if (id.HasValue)
            {
                var gameType = _gameTypeRepository.GetBy(id.Value);
                return _mapper.Map(gameType);
            }
            return new GameTypeViewModel();
        }

        public void Save(GameTypeViewModel model)
        {
            var gameType = _gameTypeRepository.GetBy(model.Id);
            if (gameType != null)
                Update(gameType, model);
            else
                Add(model);
        }

        private void Update(GameType gameType, GameTypeViewModel model)
        {
            gameType.Description = model.Description;
            _gameTypeRepository.Update(gameType);
        }

        private void Add(GameTypeViewModel model)
        {
            var gameType = _mapper.Map(model);
            _gameTypeRepository.Add(gameType);
        }
    }
}