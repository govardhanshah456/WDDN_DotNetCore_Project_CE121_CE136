using Lib_Management_Data;
using Lib_Management_Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryServices1
{
    class CustomerService : ICustomer
    {
        private Lib_ManagementContext _context;
        public CustomerService(Lib_ManagementContext context)
        {
            _context = context;
        }

        public void Add(Customer newCustomer)
        {
            _context.Add(newCustomer);
            _context.SaveChanges();
        }

        public ICustomer Get(int id)
        {
            return (ICustomer)GetAll()
                .FirstOrDefault(customer => customer.Id == id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers
                .Include(customer => customer.LibraryCard)
                .Include(customer => customer.HomeLibraryBranch);
        }

        public IEnumerable<CheckoutHistory> GetCheckoutHistory(int customerId)
        {
            var cardId = _context.Customers
                .Include(customer => customer.LibraryCard)
                .FirstOrDefault(customer => customer.Id == customerId)
                .LibraryCard.Id;

            return _context.CheckoutHistories
                .Include(co => co.LibraryCard)
                .Include(co => co.LibraryAsset)
                .Where(co => co.LibraryCard.Id == cardId)
                .OrderByDescending(co => co.CheckedOut);
        }

        public IEnumerable<Checkout> GetCheckouts(int customerId)
        {
            var cardId = _context.Customers
                .Include(customer => customer.LibraryCard)
                .FirstOrDefault(customer => customer.Id == customerId)
                .LibraryCard.Id;


            return _context.Checkouts
                .Include(co => co.LibraryCard)
                .Include(co => co.LibraryAsset)
                .Where(co => co.LibraryCard.Id == cardId);
        }

        public IEnumerable<Hold> GetHolds(int customerId)
        {
            var cardId = _context.Customers
                .Include(customer => customer.LibraryCard)
                .FirstOrDefault(customer => customer.Id == customerId)
                .LibraryCard.Id;

            return _context.Holds
                .Include(h => h.LibraryCard)
                .Include(h => h.LibraryAsset)
                .Where(h => h.LibraryCard.Id == cardId)
                .OrderByDescending(h => h.HoldPlaced);
        }

        Customer ICustomer.Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
