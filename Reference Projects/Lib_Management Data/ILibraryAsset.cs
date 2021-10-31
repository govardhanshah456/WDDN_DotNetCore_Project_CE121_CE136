using Lib_Management_Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Management_Data
{
    public interface ILibraryAsset
    {
        IEnumerable<LibraryAsset> GetAll();
        LibraryAsset GetByID(int Id);
        void Add(LibraryAsset NewAsset);
        string GetAuthorOrDirector(int Id);
        string GetDeweyIndex(int Id);
        string GetTitle(int Id);
        string GetType(int Id);
        string GetISBN(int Id);
        LibraryBranch GetCurrentLocation(int Id);
    }
}
