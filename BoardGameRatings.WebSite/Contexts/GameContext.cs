using System.Collections.Generic;
using System.Linq;
using BoardGameRatings.WebSite.Mappers;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.Models.Repositories;
using BoardGameRatings.WebSite.ViewModels;
using Microsoft.AspNet.Mvc.Rendering;

namespace BoardGameRatings.WebSite.Contexts
{
    public class GameContext : IGameContext
    {
        private readonly ICategoryMapper _categoryMapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IGameMapper _gameMapper;
        private readonly IGameRepository _gameRepository;

        public GameContext(IGameRepository gameRepository, ICategoryRepository categoryRepository,
            IGameMapper gameMapper, ICategoryMapper categoryMapper)
        {
            _categoryMapper = categoryMapper;
            _categoryRepository = categoryRepository;
            _gameRepository = gameRepository;
            _gameMapper = gameMapper;
        }


        public GameViewModel BuildViewModel(int? id = null)
        {
            var categorySelectListItems = GetCategorySelectListItems();
            var game = _gameRepository.GetBy(id ?? 0);
            var electedCategories = GetElectedCategories(id);
            return _gameMapper.Map(game, categorySelectListItems, electedCategories);
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
            var game = _gameMapper.Map(model);
            _gameRepository.Add(game);
        }

        private IEnumerable<SelectListItem> GetCategorySelectListItems()
        {
            var categories = _categoryRepository.GetAll();
            return categories.Select(c => _categoryMapper.SelectMap(c)).ToList();
        }


        private IEnumerable<CategoryViewModel> GetElectedCategories(int? id)
        {
            var gameCategories = _gameRepository.GetAllCategoriesBy(id ?? 0);
            return gameCategories.Select(c => _categoryMapper.Map(c));
        }
    }
}