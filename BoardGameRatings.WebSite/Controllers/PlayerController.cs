using BoardGameRatings.WebSite.Classes;
using BoardGameRatings.WebSite.Contexts;
using BoardGameRatings.WebSite.ViewModels;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Routing;

namespace BoardGameRatings.WebSite.Controllers
{
    public class PlayerController : Controller
    {
        private static readonly string CONTROLLER_NAME = "Player";
        private static readonly string INDEX_ACTION_NAME = "Index";
        private static readonly string SAVE_ACTION_NAME = "Save";
        private static readonly string ADD_ACTION_NAME = "Add";
        private static readonly string REMOVE_ACTION_NAME = "Remove";
        private static readonly string ID_PARAMETER_NAME = "id";
        private static readonly string PLAYER_ID_PARAMETER_NAME = "playerId";
        private static readonly string GAME_ID_PARAMETER_NAME = "gameId";
        private static readonly string MODEL_PARAMETER_NAME = "model";
        private readonly IPlayerContext _context;

        public PlayerController(IPlayerContext context)
        {
            _context = context;
        }

        public static RouteValueDictionary BuildIndexActionRouteValues()
        {
            return
                new RouteValueDictionaryBuilder()
                    .WithController(CONTROLLER_NAME)
                    .WithAction(INDEX_ACTION_NAME)
                    .Build();
        }

        public static RouteValueDictionary BuildIndexActionRouteValues(int id)
        {
            return
                new RouteValueDictionaryBuilder()
                    .WithController(CONTROLLER_NAME)
                    .WithAction(INDEX_ACTION_NAME)
                    .WithParameter(ID_PARAMETER_NAME, id)
                    .Build();
        }

        public IActionResult Index(int? id = null)
        {
            var viewModel = _context.BuildViewModel(id);
            return View(viewModel);
        }

        public RedirectToRouteResult Save(PlayerViewModel viewModel)
        {
            _context.Save(viewModel);
            return RedirectToRoute(PlayersController.BuildIndexActionRouteValues());
        }

        public RedirectToRouteResult Add(PlayerViewModel playerViewModel)
        {
            _context.AddGameOwned(playerViewModel.Id, playerViewModel.GameId);
            return RedirectToRoute(BuildIndexActionRouteValues(playerViewModel.Id));
        }

        public RedirectToRouteResult Remove(int playerId, int gameId)
        {
            _context.RemoveGameOwned(playerId, gameId);
            return RedirectToRoute(BuildIndexActionRouteValues(playerId));
        }

        public static RouteValueDictionary BuildSaveActionRouteValues(PlayerViewModel model)
        {
            return new RouteValueDictionaryBuilder()
                .WithController(CONTROLLER_NAME)
                .WithAction(SAVE_ACTION_NAME)
                .WithParameter(MODEL_PARAMETER_NAME, model)
                .Build();
        }

        public static RouteValueDictionary BuildAddActionRouteValues(PlayerViewModel model)
        {
            return new RouteValueDictionaryBuilder()
                .WithController(CONTROLLER_NAME)
                .WithAction(ADD_ACTION_NAME)
                .WithParameter(MODEL_PARAMETER_NAME, model)
                .Build();
        }

        public static RouteValueDictionary BuildRemoveActionRouteValues(int playerId, int gameId)
        {
            return new RouteValueDictionaryBuilder()
                .WithController(CONTROLLER_NAME)
                .WithAction(REMOVE_ACTION_NAME)
                .WithParameter(PLAYER_ID_PARAMETER_NAME, playerId)
                .WithParameter(GAME_ID_PARAMETER_NAME, gameId)
                .Build();
        }
    }
}