using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lib_Management.Models.Customer
{
    public class CustomerIndexModel
    {
        public IEnumerable<CustomerDetailModel> Customers { get; set; }

    }
}
