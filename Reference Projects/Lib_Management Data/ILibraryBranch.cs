using Lib_Management_Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Management_Data
{
    public interface ILibraryBranch
    {
        int Id { get; set; }
        string name { get; set; }
        object ImageUrl { get; set; }
        string Address { get; set; }
        string Telephone { get; set; }
        object OpenDate { get; set; }

        IEnumerable<LibraryBranch> GetAll();
        IEnumerable<Customer> GetCustomers(int branchId);
        IEnumerable<LibraryAsset> GetAssets(int branchId);
        IEnumerable<string> GetBranchHours(int branchId);
        ILibraryBranch Get(int branchId);
        void Add(ILibraryBranch newBranch);
        bool IsBranchOpen(int branchId);

    }
}
