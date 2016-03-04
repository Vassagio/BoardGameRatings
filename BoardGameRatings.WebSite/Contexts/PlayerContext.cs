using System.Collections.Generic;
using System.Linq;
using BoardGameRatings.WebSite.Mappers;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.Models.Repositories;
using BoardGameRatings.WebSite.ViewModels;
using Microsoft.AspNet.Mvc.Rendering;

namespace BoardGameRatings.WebSite.Contexts
{
    public class PlayerContext : IPlayerContext
    {
        private readonly IGameMapper _gameMapper;
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerMapper _playerMapper;
        private readonly IPlayerRepository _playerRepository;

        public PlayerContext(IPlayerRepository playerRepository, IGameRepository gameRepository,
            IPlayerMapper playerMapper, IGameMapper gameMapper)
        {
            _playerRepository = playerRepository;
            _gameRepository = gameRepository;
            _playerMapper = playerMapper;
            _gameMapper = gameMapper;
        }


        public PlayerViewModel BuildViewModel(int? id = null)
        {
            var gameSelectListItems = GetGameSelectListItems();
            var player = _playerRepository.GetBy(id ?? 0);
            var gamesOwned = GetGamesOwned(id);
            return _playerMapper.Map(player, gameSelectListItems, gamesOwned);
        }

        public void Save(PlayerViewModel model)
        {
            var player = _playerRepository.GetBy(model.Id);
            if (player != null)
                Update(player, model);
            else
                Add(model);
        }

        public void AddGameOwned(int playerId, int gameId)
        {
            _playerRepository.AddGameOwned(playerId, gameId);
        }

        private IEnumerable<GameViewModel> GetGamesOwned(int? id)
        {
            var playerGames = _playerRepository.GetAllGamesBy(id ?? 0);
            return playerGames.Select(g => _gameMapper.Map(g));
        }

        private IEnumerable<SelectListItem> GetGameSelectListItems()
        {
            var games = _gameRepository.GetAll();
            return games.Select(g => _gameMapper.SelectMap(g)).ToList();
        }

        private void Update(Player player, PlayerViewModel model)
        {
            player.FirstName = model.FirstName;
            player.LastName = model.LastName;
            _playerRepository.Update(player);
        }

        private void Add(PlayerViewModel model)
        {
            var player = _playerMapper.Map(model);
            _playerRepository.Add(player);
        }
    }
}