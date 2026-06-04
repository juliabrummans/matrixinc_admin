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

        // GET: Customers
        // A: Parameter sortOrder toegevoegd om de gekozen sortering te ontvangen
        public IActionResult Index(string searchString, string statusFilter, string sortOrder)
        {
            var customers = _customerRepo.GetAllCustomers();

            // Sla de huidige filters en sortering op voor de frontend links
            ViewData["CurrentSearch"] = searchString;
            ViewData["CurrentStatusFilter"] = statusFilter;
            ViewData["CurrentSort"] = sortOrder;

            // N: Wissel klaarzetten voor het omdraaien van de sorteerrichting bij een klik
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AddressSortParm"] = sortOrder == "Address" ? "address_desc" : "Address";
            ViewData["OrderSortParm"] = sortOrder == "Order" ? "order_desc" : "Order";

            // Filter op zoekterm
            if (!string.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(c => c.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                              || c.Address.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            // Filter op status open of geen bestelling
            if (!string.IsNullOrEmpty(statusFilter))
            {
                bool hasOrder = statusFilter == "Active";
                customers = customers.Where(c => c.Active == hasOrder);
            }

            // N: Sorteren op basis van de gekozen sortering
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

        // GET: Customers/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var customer = _customerRepo.GetCustomerById(id.Value);
            if (customer == null) return NotFound();

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
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

        // GET: Customers/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var customer = _customerRepo.GetCustomerById(id.Value);
            if (customer == null) return NotFound();

            return View(customer);
        }

        // POST: Customers/Edit/5
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

        // GET: Customers/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var customer = _customerRepo.GetCustomerById(id.Value);
            if (customer == null) return NotFound();

            return View(customer);
        }

        // POST: Customers/Delete/5
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