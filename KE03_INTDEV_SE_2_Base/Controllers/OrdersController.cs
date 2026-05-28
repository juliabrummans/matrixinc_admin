using KE03_INTDEV_SE_2_Base.Models;
using Microsoft.AspNetCore.Mvc;

namespace KE03_INTDEV_SE_2_Base.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            var orders = new List<Order>
            {
                new Order { Id = 1, ProductNaam = "Laptop", Aantal = 1, Datum = DateTime.Now },
                new Order { Id = 2, ProductNaam = "Muis", Aantal = 2, Datum = DateTime.Now }
            };

            return View(orders);
        }
    }
}