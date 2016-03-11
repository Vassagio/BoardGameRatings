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
        private static readonly string ADD_ACTION_NAME = "Add";
        private static readonly string ID_PARAMETER_NAME = "id";
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
    
        public RedirectToRouteResult Add(GameViewModel gameViewModel)
        {
            _context.AddElectedCategory(gameViewModel.Id, gameViewModel.CategoryId);
            return RedirectToRoute(BuildIndexActionRouteValues(gameViewModel.Id));
        }

        public static RouteValueDictionary BuildAddActionRouteValues(GameViewModel model)
        {            ;
            return new RouteValueDictionaryBuilder()
                .WithController(CONTROLLER_NAME)
                .WithAction(ADD_ACTION_NAME)
                .WithParameter(MODEL_PARAMETER_NAME, model)
                .Build();
        }
    }
}