using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace KE03_INTDEV_SE_2_Base
{
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _customerRepo;

        public CustomersController(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        
        public IActionResult Index(string searchString, string statusFilter, string sortOrder)
        {
            var customers = _customerRepo.GetAllCustomers();

            ViewData["CurrentSearch"] = searchString;
            ViewData["CurrentStatusFilter"] = statusFilter;
            ViewData["CurrentSort"] = sortOrder;

            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AddressSortParm"] = sortOrder == "Address" ? "address_desc" : "Address";
            ViewData["OrderSortParm"] = sortOrder == "Order" ? "order_desc" : "Order";

            if (!string.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(c => c.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                              || c.Address.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            // Filteren op openstaande orders active
            if (!string.IsNullOrEmpty(statusFilter))
            {
                bool hasOrder = statusFilter == "Active";
                customers = customers.Where(c => c.Active == hasOrder);
            }

            // Sorteren op basis van de gekozen sortering
            switch (sortOrder)
            {
                case "name_desc":
                    customers = customers.OrderByDescending(c => c.Name);
                    break;
                case "Address":
                    customers = customers.OrderBy(c => c.Address);
                    break;
                case "address_desc":
                    customers = customers.OrderByDescending(c => c.Address);
                    break;
                case "Order":
                    customers = customers.OrderBy(c => c.Active);
                    break;
                case "order_desc":
                    customers = customers.OrderByDescending(c => c.Active);
                    break;
                default:
                    customers = customers.OrderBy(c => c.Name);
                    break;
            }

            return View(customers);
        }

        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var customer = _customerRepo.GetCustomerById(id.Value);
            if (customer == null) return NotFound();

            return View(customer);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Address,Active")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerRepo.AddCustomer(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var customer = _customerRepo.GetCustomerById(id.Value);
            if (customer == null) return NotFound();

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Address,Active")] Customer customer)
        {
            if (id != customer.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _customerRepo.UpdateCustomer(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var customer = _customerRepo.GetCustomerById(id.Value);
            if (customer == null) return NotFound();

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var customer = _customerRepo.GetCustomerById(id);
            if (customer != null)
            {
                _customerRepo.DeleteCustomer(customer);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}