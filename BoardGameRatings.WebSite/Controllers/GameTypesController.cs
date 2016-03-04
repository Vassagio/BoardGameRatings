using BoardGameRatings.WebSite.Classes;
using BoardGameRatings.WebSite.Contexts;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Routing;

namespace BoardGameRatings.WebSite.Controllers
{
    [Route("[controller]")]
    public class GameTypesController : Controller
    {
        private static readonly string CONTROLLER_NAME = "GameTypes";
        private static readonly string INDEX_ACTION_NAME = "Index";
        private static readonly string REMOVE_ACTION_NAME = "Remove";
        private static readonly string EDIT_ACTION_NAME = "Edit";
        private static readonly string ADD_ACTION_NAME = "Add";
        private static readonly string ID_PARAMETER_NAME = "id";
        private readonly IGameTypesContext _context;

        public GameTypesController(IGameTypesContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var viewModel = _context.BuildViewModel();
            return View(viewModel);
        }

        [Route("[action]/{id}")]
        public RedirectToRouteResult Remove(int id)
        {
            _context.Remove(id);
            return RedirectToRoute(BuildIndexActionRouteValues());
        }

        [Route("[action]/{id}")]
        public RedirectToRouteResult Edit(int id)
        {
            return RedirectToRoute(GameTypeController.BuildIndexActionRouteValues(id));
        }

        [Route("[action]")]
        public RedirectToRouteResult Add()
        {
            return RedirectToRoute(GameTypeController.BuildIndexActionRouteValues());
        }

        public static RouteValueDictionary BuildIndexActionRouteValues()
        {
            return new RouteValueDictionaryBuilder()
                .WithController(CONTROLLER_NAME)
                .WithAction(INDEX_ACTION_NAME)
                .Build();
        }

        public static RouteValueDictionary BuildRemoveActionRouteValues(int id)
        {
            return new RouteValueDictionaryBuilder()
                .WithController(CONTROLLER_NAME)
                .WithAction(REMOVE_ACTION_NAME)
                .WithParameter(ID_PARAMETER_NAME, id)
                .Build();
        }

        public static RouteValueDictionary BuildEditActionRouteValues(int id)
        {
            return new RouteValueDictionaryBuilder()
                .WithController(CONTROLLER_NAME)
                .WithAction(EDIT_ACTION_NAME)
                .WithParameter(ID_PARAMETER_NAME, id)
                .Build();
        }

        public static RouteValueDictionary BuildAddActionRouteValues()
        {
            return new RouteValueDictionaryBuilder()
                .WithController(CONTROLLER_NAME)
                .WithAction(ADD_ACTION_NAME)
                .Build();
        }
    }
}