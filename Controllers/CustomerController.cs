using Lib_Management_Data;
using Lib_Management.Models.Customer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lib_Management_Data.Models;

namespace Lib_Management.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomer _customer;

        public IEnumerable<Lib_Management_Data.Models.CheckoutHistory> ChekoutHistory { get; private set; }
        public IEnumerable<Lib_Management_Data.Models.Hold> Holds { get; private set; }

        public CustomerController(ICustomer customer)
        {
            _customer = customer;
        }
        public IActionResult Index()
        {
            var allCustomers = _customer.GetAll();

            var customerModels = allCustomers.Select(p => new CustomerDetailModel
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                LibraryCardId = p.LibraryCard.Id,
                OverdueFees = p.LibraryCard.Fees,
                HomeLibraryBranch = p.HomeLibraryBranch.Name
            }).ToList();

            var model = new CustomerIndexModel()
            {
                Customers = customerModels
            };

            return View(model);
        }
        public IActionResult Detail(int Id)
        {
            var customer = _customer.Get(Id);
            var model = new CustomerDetailModel
            {
                LastName = customer.LastName,
                FirstName = customer.FirstName,
                Address = customer.Address,
                HomeLibraryBranch = customer.HomeLibraryBranch.Name,
                MemberSince = customer.LibraryCard.Created,
                OverdueFees = customer.LibraryCard.Fees,
                LibraryCardId = customer.LibraryCard.Id,
                Telephone = customer.Telephone,
                AssetsCheckedOut = _customer.GetCheckouts(Id).ToList() ?? new List<Checkout>(),
                ChekoutHistory = _customer.GetCheckoutHistory(Id),
                Holds = _customer.GetHolds(Id)
            };

            return View(model);
        }

    internal class Chekout
    {
    }
}
}
