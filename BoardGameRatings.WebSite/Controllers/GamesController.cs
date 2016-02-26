using BoardGameRatings.WebSite.Classes;
using BoardGameRatings.WebSite.Contexts;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Routing;

namespace BoardGameRatings.WebSite.Controllers
{
    public class GamesController : Controller
    {
        private static readonly string CONTROLLER_NAME = "Games";
        private static readonly string INDEX_ACTION_NAME = "Index";
        private static readonly string REMOVE_ACTION_NAME = "Remove";
        private static readonly string EDIT_ACTION_NAME = "Edit";
        private static readonly string ID_PARAMETER_NAME = "id";
        private readonly IGamesContext _context;

        public GamesController(IGamesContext context)
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
        public RedirectToRouteResult Edit(int id) {
            _context.Remove(id);
            return RedirectToRoute(BuildIndexActionRouteValues());
        }

        public static RouteValueDictionary BuildIndexActionRouteValues()
        {
            return
                new RouteValueDictionaryBuilder().WithController(CONTROLLER_NAME).WithAction(INDEX_ACTION_NAME).Build();
        }

        public static RouteValueDictionary BuildRemoveActionRouteValues(int id)
        {
            return
                new RouteValueDictionaryBuilder().WithController(CONTROLLER_NAME)
                    .WithAction(REMOVE_ACTION_NAME)
                    .WithParameter(ID_PARAMETER_NAME, id)
                    .Build();
        }

        public static RouteValueDictionary BuildEditActionRouteValues(int id)
        {
            return new RouteValueDictionaryBuilder().WithController(CONTROLLER_NAME)
                .WithAction(EDIT_ACTION_NAME)
                .WithParameter(ID_PARAMETER_NAME, id)
                .Build();
        }
    }
}