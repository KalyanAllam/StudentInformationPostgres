using Microsoft.AspNetCore.Mvc;
using SISPostgres.Models;
using System.Diagnostics;

namespace SISPostgres.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (TempData.Peek("Message") == null)
            {
                var routeValue = new RouteValueDictionary(new { action = "Index", controller = "Login" });
                return RedirectToRoute(routeValue);
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}