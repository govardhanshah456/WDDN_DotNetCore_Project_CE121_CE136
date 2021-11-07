using Lib_Management_Data;
using Lib_Management_Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace LibraryServices1
{
    public class LibraryBranchService : ILibraryBranch
    {
        private Lib_ManagementContext _context;

        public LibraryBranchService(Lib_ManagementContext context)
        {
            _context = context;
        }

        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public object ImageUrl { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Address { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Telephone { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public object OpenDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //public object OpenDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Add(ILibraryBranch newBranch)
        {
            _context.Add(newBranch);
            _context.SaveChanges();
        }

        public ILibraryBranch Get(int branchId)
        {
            return (ILibraryBranch)_context.LibraryBranches
                .Include(b => b.Customers)
                .Include(b => b.LibraryAssets)
                .FirstOrDefault(b => b.Id == branchId);
        }

        public IEnumerable<LibraryBranch> GetAll()
        {
            return _context.LibraryBranches
                .Include(b => b.Customers)
                .Include(b => b.LibraryAssets);
        }

        public IEnumerable<LibraryAsset> GetAssets(int branchId)
        {
            return _context.LibraryBranches
                .Include(b => b.LibraryAssets)
                .FirstOrDefault(b => b.Id == branchId)
                .LibraryAssets;
        }

        public IEnumerable<string> GetBranchHours(int branchId)
        {
            var hours = _context.BranchHours.Where(h => h.Branch.Id == branchId);
            return DataHelpers.HumanizeBizHours(hours);
        }

        public IEnumerable<Customer> GetCustomers(int branchId)
        {
            return _context.LibraryBranches
                .Include(b => b.Customers)
                .FirstOrDefault(b => b.Id == branchId)
                .Customers;

        }

        public bool IsBranchOpen(int branchId)
        {
            var currentTimeHour = DateTime.Now.Hour;
            var currentDayOfWeek = (int)DateTime.Now.DayOfWeek + 1;
            var hours = _context.BranchHours.Where(h => h.Branch.Id == branchId);
            var dayHours = hours.FirstOrDefault(h => h.DayOfWeek == currentDayOfWeek);

            return currentTimeHour < dayHours.CloseTime && currentTimeHour > dayHours.OpenTime;

        }
    }
}
