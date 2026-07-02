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
                OrderId = 1042, DropLocation = " F.01 ", Status = "Te verwerken", StatusColor = "danger",
                Items = new List<OrderPickViewModel.PickItemViewModel> {
                    new OrderPickViewModel.PickItemViewModel { ProductId = 1, Quantity = 5, ProductName = "Verzinkte spaanplaatschroef 3,0 x 12 mm Torx", ProductLocation = "B.12.02", IsPicked = false },
                    new OrderPickViewModel.PickItemViewModel { ProductId = 2, Quantity = 2, ProductName = "Hardhoutschroef RVS A4 5 x 60 mm Torx 25", ProductLocation = "B.12.01", IsPicked = false }
                }
            },
            new OrderPickViewModel {
                OrderId = 1043, DropLocation = "F.02", Status = "Te verwerken", StatusColor = "danger",
                Items = new List<OrderPickViewModel.PickItemViewModel> {
                    new OrderPickViewModel.PickItemViewModel { ProductId = 3, Quantity = 10, ProductName = "Schroef a34 ", ProductLocation = "C.05.09", IsPicked = false }
                }
            },
            new OrderPickViewModel {
                OrderId = 1044, DropLocation = "Verzonden", Status = "Afgehandeld", StatusColor = "success",
                Items = new List<OrderPickViewModel.PickItemViewModel> {
                    new OrderPickViewModel.PickItemViewModel { ProductId = 4, Quantity = 3, ProductName = "Peer", ProductLocation = "A.01.05", IsPicked = true }
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