using Lib_Management_Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Management_Data
{
    public interface IChekout
    {
        IEnumerable<Checkout> GetAll();
        Checkout GetById(int Id);
        void Add(Checkout newCheckout);
        void CheckOutItem(int assetId, int LibraryCardId);
        void CheckInItem(int assetId);
        IEnumerable<CheckoutHistory> GetCheckoutHistory(int Id);

        void PlaceHold(int assetId, int LibraryCardId);
        string GetCurrentHoldCustomerName(int Id);
        DateTime GetCurrentHoldPlaced(int Id);
        IEnumerable<Hold> GetCurrentHolds(int Id);

        void MarkLost(int assetId);
        void MarkFound(int assetId);
        Checkout GetLatestCheckout(int assetId);
        string GetCurrentCheckoutCustomer(int assetId);
        bool IsCheckedOut(int Id);
    }
}
