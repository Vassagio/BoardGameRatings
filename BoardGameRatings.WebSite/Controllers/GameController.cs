using System;
using BoardGameRatings.WebSite.Classes;
using BoardGameRatings.WebSite.Contexts;
using BoardGameRatings.WebSite.ViewModels;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Routing;

namespace BoardGameRatings.WebSite.Controllers
{
    public class GameController : Controller
    {
        private static readonly string CONTROLLER_NAME = "Game";
        private static readonly string INDEX_ACTION_NAME = "Index";
        private static readonly string SAVE_ACTION_NAME = "Save";
        private static readonly string ADD_CATEGORY_ACTION_NAME = "AddCategory";
        private static readonly string ADD_PLAYED_DATE_ACTION_NAME = "AddPlayedDate";
        private static readonly string REMOVE_ACTION_NAME = "Remove";
        private static readonly string ID_PARAMETER_NAME = "id";
        private static readonly string CATEGORY_ID_PARAMETER_NAME = "categoryId";
        private static readonly string PLAYED_DATE_ID_PARAMETER_NAME = "playedDateId";
        private static readonly string GAME_ID_PARAMETER_NAME = "gameId";
        private static readonly string MODEL_PARAMETER_NAME = "model";
        private readonly IGameContext _context;

        public GameController(IGameContext context)
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

        public RedirectToRouteResult Save(GameViewModel viewModel)
        {
            _context.Save(viewModel);
            return RedirectToRoute(GamesController.BuildIndexActionRouteValues());
        }

        public static RouteValueDictionary BuildSaveActionRouteValues(GameViewModel model)
        {
            return new RouteValueDictionaryBuilder()
                .WithController(CONTROLLER_NAME)
                .WithAction(SAVE_ACTION_NAME)
                .WithParameter(MODEL_PARAMETER_NAME, model)
                .Build();
        }

        public RedirectToRouteResult AddCategory(GameViewModel gameViewModel)
        {
            _context.AddElectedCategory(gameViewModel.Id, gameViewModel.CategoryId);
            return RedirectToRoute(BuildIndexActionRouteValues(gameViewModel.Id));
        }

        public RedirectToRouteResult AddPlayedDate(GameViewModel gameViewModel)
        {
            _context.AddPlayedDate(gameViewModel.Id, gameViewModel.SelectedPlayedDate);
            return RedirectToRoute(BuildIndexActionRouteValues(gameViewModel.Id));
        }


        public RedirectToRouteResult Remove(int gameId, int categoryId)
        {
            _context.RemoveElectedCategory(gameId, categoryId);
            return RedirectToRoute(BuildIndexActionRouteValues(gameId));
        }

        public RedirectToRouteResult Remove(int gameId, DateTime playedDate)
        {
            _context.RemovePlayedDate(gameId, playedDate);
            return RedirectToRoute(BuildIndexActionRouteValues(gameId));
        }

        public static RouteValueDictionary BuildAddCategoryActionRouteValues(GameViewModel model)
        {
            return new RouteValueDictionaryBuilder()
                .WithController(CONTROLLER_NAME)
                .WithAction(ADD_CATEGORY_ACTION_NAME)
                .WithParameter(MODEL_PARAMETER_NAME, model)
                .Build();
        }

        public static RouteValueDictionary BuildAddPlayedDateActionRouteValues(GameViewModel model)
        {
            return new RouteValueDictionaryBuilder()
                .WithController(CONTROLLER_NAME)
                .WithAction(ADD_PLAYED_DATE_ACTION_NAME)
                .WithParameter(MODEL_PARAMETER_NAME, model)
                .Build();
        }

        public static RouteValueDictionary BuildRemoveCategoryActionRouteValues(int gameId, int categoryId)
        {
            return new RouteValueDictionaryBuilder()
                .WithController(CONTROLLER_NAME)
                .WithAction(REMOVE_ACTION_NAME)
                .WithParameter(GAME_ID_PARAMETER_NAME, gameId)
                .WithParameter(CATEGORY_ID_PARAMETER_NAME, categoryId)
                .Build();
        }

        public static RouteValueDictionary BuildRemovePlayedDateActionRouteValues(int gameId, DateTime playedDate)
        {
            return new RouteValueDictionaryBuilder()
                .WithController(CONTROLLER_NAME)
                .WithAction(REMOVE_ACTION_NAME)
                .WithParameter(GAME_ID_PARAMETER_NAME, gameId)
                .WithParameter(PLAYED_DATE_ID_PARAMETER_NAME, playedDate)
                .Build();
        }
    }
}