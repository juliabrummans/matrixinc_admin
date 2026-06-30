using Microsoft.AspNetCore.Mvc;
using KE03_INTDEV_SE_2_Base.Models;
using System.Collections.Generic;
using System.Linq;

namespace KE03_INTDEV_SE_2_Base.Controllers
{
    public class OrdersController : Controller
    {
        public static List<OrderPickViewModel> MockOrders = new List<OrderPickViewModel>
        {
            new OrderPickViewModel {
                OrderId = 1042, DropLocation = "Krat 1", Status = "Te verwerken", StatusColor = "danger",
                Items = new List<PickItemViewModel> {
                    new PickItemViewModel { ProductId = 1, Quantity = 5, ProductName = "Banaan", ProductLocation = "A.01.04", IsPicked = false },
                    new PickItemViewModel { ProductId = 2, Quantity = 2, ProductName = "Appel", ProductLocation = "B.12.01", IsPicked = false }
                }
            },
            new OrderPickViewModel {
                OrderId = 1043, DropLocation = "Krat 2", Status = "Te verwerken", StatusColor = "danger",
                Items = new List<PickItemViewModel> {
                    new PickItemViewModel { ProductId = 3, Quantity = 10, ProductName = "Mandarijn", ProductLocation = "C.05.09", IsPicked = false }
                }
            },
            new OrderPickViewModel {
                OrderId = 1044, DropLocation = "Verzonden", Status = "Afgehandeld", StatusColor = "success",
                Items = new List<PickItemViewModel> {
                    new PickItemViewModel { ProductId = 4, Quantity = 3, ProductName = "Peer", ProductLocation = "A.01.05", IsPicked = true }
                }
            }
        };

        [HttpGet]
        public IActionResult Index()
        {
            var tePicken = MockOrders.Where(o => o.Status == "Te verwerken").ToList();
            return View(tePicken);
        }

        [HttpGet]
        public IActionResult Historie()
        {
            var afgehandeld = MockOrders.Where(o => o.Status == "Afgehandeld").ToList();
            return View(afgehandeld);
        }

        [HttpGet]
        public IActionResult PickOrder(int id)
        {
            var order = MockOrders.FirstOrDefault(o => o.OrderId == id);
            if (order == null) return NotFound();
            return View(order);
        }

        [HttpPost]
        public IActionResult PickOrder(OrderPickViewModel model)
        {
            if (ModelState.IsValid)
            {
                var order = MockOrders.FirstOrDefault(o => o.OrderId == model.OrderId);
                if (order != null)
                {
                    order.Status = "Afgehandeld";
                    order.StatusColor = "success";
                    foreach (var item in order.Items) { item.IsPicked = true; }
                }

                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}