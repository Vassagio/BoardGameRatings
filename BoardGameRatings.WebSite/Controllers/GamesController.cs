using BoardGameRatings.WebSite.Contexts;
using Microsoft.AspNet.Mvc;

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
    }
}