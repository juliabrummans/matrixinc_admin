using Microsoft.AspNetCore.Mvc;

namespace KE03_INTDEV_SE_2_Base.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Laadt het originele dashboard met de vierkanten
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}