using BoardGameRatings.WebSite.Classes;
using BoardGameRatings.WebSite.Contexts;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Routing;

namespace BoardGameRatings.WebSite.Controllers
{
    public class GamesController : Controller
    {
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

        public static RouteValueDictionary BuildIndexActionRouteValues()
        {
            return new RouteValueDictionaryBuilder().WithController("Games").WithAction("Index").Build();
        }

        public static RouteValueDictionary BuildRemoveActionRouteValues(int id)
        {
            return
                new RouteValueDictionaryBuilder().WithController("Games")
                    .WithAction("Remove")
                    .WithParameter("id", id)
                    .Build();
        }
    }
}