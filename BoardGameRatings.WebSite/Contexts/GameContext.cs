using BoardGameRatings.WebSite.Mappers;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.Models.Repositories;
using BoardGameRatings.WebSite.ViewModels;

namespace BoardGameRatings.WebSite.Contexts
{
    public class GameContext : IGameContext
    {
        private readonly IGameRepository _gameRepository;
        private readonly IGameMapper _mapper;

        public GameContext(IGameRepository gameRepository, IGameMapper mapper)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }


        public GameViewModel BuildViewModel(int? id = null)
        {
            if (id.HasValue)
            {
                var game = _gameRepository.GetBy(id.Value);
                return _mapper.Map(game);
            }
            return new GameViewModel();
        }

        public void Save(GameViewModel model)
        {
            var game = _gameRepository.GetBy(model.Id);
            if (game != null)
                Update(game, model);
            else
                Add(model);
        }

        private void Update(Game game, GameViewModel model)
        {
            game.Name = model.Name;
            game.Description = model.Description;
            _gameRepository.Update(game);
        }

        private void Add(GameViewModel model)
        {
            var game = _mapper.Map(model);
            _gameRepository.Add(game);
        }
    }
}