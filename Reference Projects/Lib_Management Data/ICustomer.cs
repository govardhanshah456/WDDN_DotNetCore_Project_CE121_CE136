using Lib_Management_Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Management_Data
{
    public interface ICustomer
    {

        Customer Get(int id);
        IEnumerable<Customer> GetAll();
        void Add(Customer newCustomer);

        IEnumerable<CheckoutHistory> GetCheckoutHistory(int customerId);
        IEnumerable<Hold> GetHolds(int customerId);
        IEnumerable<Checkout> GetCheckouts(int customerId);
    }
}
