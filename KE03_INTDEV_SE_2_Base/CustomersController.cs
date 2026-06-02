using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
// n: nieuw   a : aangepaste code 
namespace KE03_INTDEV_SE_2_Base
{
    public class CustomersController : Controller
    {
        // N: gebruik nu de ICustomerRepository interface in plaats van de directe MatrixIncDbContext.
        private readonly ICustomerRepository _customerRepo;

        // N: De constructor ontvangt de repository nu via Dependency Injection.
        public CustomersController(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        // GET: Customers
        public IActionResult Index(string searchString, string statusFilter)
        {
            var customers = _customerRepo.GetAllCustomers();

            // filter op naam of adres
            if (!string.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(c => c.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                              || c.Address.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            
            if (!string.IsNullOrEmpty(statusFilter))
            {
                bool isActive = statusFilter == "Active";
                customers = customers.Where(c => c.Active == isActive);
            }

            //gekozen filters terug naar de view zodat ze in de balk blijven staan
            ViewData["CurrentSearch"] = searchString;
            ViewData["CurrentStatusFilter"] = statusFilter;

            return View(customers);
        }

        // GET: Customers/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // A: Specifieke klant wordt opgehaald via de repository methode GetCustomerById.
            var customer = _customerRepo.GetCustomerById(id.Value);
            if (customer == null)
            {
                return NotFound();
            }

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
                // A: Het opslaan van een nieuwe klant verloopt nu via de repository.
                _customerRepo.AddCustomer(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // a: Bestaande gegevens ophalen via de repository om het formulier te vullen.
            var customer = _customerRepo.GetCustomerById(id.Value);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Address,Active")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // A: Wijzigingen opslaan via de repository methode UpdateCustomer.
                _customerRepo.UpdateCustomer(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // A: Gegevens ophalen via de repository om de bevestigingspagina te tonen.
            var customer = _customerRepo.GetCustomerById(id.Value);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // A: Eerst de klant ophalen via de repository omdat DeleteCustomer het volledige model verwacht.
            var customer = _customerRepo.GetCustomerById(id);
            if (customer != null)
            {
                // A: Verwijdering doorvoeren via de repository.
                _customerRepo.DeleteCustomer(customer);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}