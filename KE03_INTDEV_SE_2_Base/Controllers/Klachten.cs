using System;
using Microsoft.AspNetCore.Mvc;

namespace KE03_INTDEV_SE_2_Base.Controllers
{
    public class KlachtenController : Controller
    {
        // GET: Home/Index
        public IActionResult Index()
        {
            return View();
        }
    }
}