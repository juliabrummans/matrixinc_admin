using System;
using Microsoft.AspNetCore.Mvc;

namespace KE03_INTDEV_SE_2_Base.Controllers
{
    public class BezorgersController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }
    }
}