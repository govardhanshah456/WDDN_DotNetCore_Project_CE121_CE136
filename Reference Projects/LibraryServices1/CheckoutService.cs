using Lib_Management_Data;
using Lib_Management_Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace LibraryServices1
{
    public class CheckoutService : IChekout
    {
        private Lib_ManagementContext _context;
        public CheckoutService(Lib_ManagementContext context)
        {
            _context = context;
        }
        public void Add(Checkout newCheckout)
        {
            _context.Add(newCheckout);
            _context.SaveChanges();
        }

        public void CheckInItem(int assetId)
        {
            var now = DateTime.Now;

            var item = _context.LibraryAssets
                .First(a => a.Id == assetId);

            _context.Update(item);

            // remove any existing checkouts on the item
            var checkout = _context.Checkouts
                .Include(c => c.LibraryAsset)
                .Include(c => c.LibraryCard)
                .FirstOrDefault(a => a.LibraryAsset.Id == assetId);
            if (checkout != null) _context.Remove(checkout);

            // close any existing checkout history
            var history = _context.CheckoutHistories
                .Include(h => h.LibraryAsset)
                .Include(h => h.LibraryCard)
                .FirstOrDefault(h =>
                    h.LibraryAsset.Id == assetId
                    && h.CheckedIn == null);
            if (history != null)
            {
                _context.Update(history);
                history.CheckedIn = now;
            }

            // look for current holds
            var currentHolds = _context.Holds
                .Include(a => a.LibraryAsset)
                .Include(a => a.LibraryCard)
                .Where(a => a.LibraryAsset.Id == assetId);

            // if there are current holds, check out the item to the earliest
            if (currentHolds.Any())
            {
                CheckoutToEarliestHold(assetId, currentHolds);
                return;
            }

            // otherwise, set item status to available
            item.Status = _context.Statuses.FirstOrDefault(a => a.Name == "Available");

            _context.SaveChanges();
        }

        private void CheckoutToEarliestHold(int assetId, IEnumerable<Hold> currentHolds)
        {
            var earliestHold = currentHolds.OrderBy(a => a.HoldPlaced).FirstOrDefault();
            if (earliestHold == null) return;
            var card = earliestHold.LibraryCard;
            _context.Remove(earliestHold);
            _context.SaveChanges();

            CheckoutItem(assetId, card.Id);
        }
        public void CheckoutItem(int id, int libraryCardId)
        {
            if (IsCheckedOut(id)) return;

            var item = _context.LibraryAssets
                .Include(a => a.Status)
                .First(a => a.Id == id);

            _context.Update(item);

            item.Status = _context.Statuses
                .FirstOrDefault(a => a.Name == "Checked Out");

            var now = DateTime.Now;

            var libraryCard = _context.LibraryCards
                .Include(c => c.Checkouts)
                .FirstOrDefault(a => a.Id == libraryCardId);

            var checkout = new Checkout
            {
                LibraryAsset = item,
                LibraryCard = libraryCard,
                Since = now,
                Until = GetDefaultCheckoutTime(now)
            };

            _context.Add(checkout);

            var checkoutHistory = new CheckoutHistory
            {
                CheckedOut = now,
                LibraryAsset = item,
                LibraryCard = libraryCard
            };

            _context.Add(checkoutHistory);
            _context.SaveChanges();
        }
        private DateTime GetDefaultCheckoutTime(DateTime now)
        {
            return now.AddDays(30);
        }
        public bool IsCheckedOut(int id)
        {
            var isCheckedOut = _context.Checkouts.Any(a => a.LibraryAsset.Id == id);

            return isCheckedOut;
        }
        public void CheckOutItem(int assetId, int LibraryCardId)
        {
            if (IsCheckedOut(assetId)) return;

            var item = _context.LibraryAssets
                .Include(a => a.Status)
                .First(a => a.Id == assetId);

            _context.Update(item);

            item.Status = _context.Statuses
                .FirstOrDefault(a => a.Name == "Checked Out");

            var now = DateTime.Now;

            var libraryCard = _context.LibraryCards
                .Include(c => c.Checkouts)
                .FirstOrDefault(a => a.Id == LibraryCardId);

            var checkout = new Checkout
            {
                LibraryAsset = item,
                LibraryCard = libraryCard,
                Since = now,
                Until = GetDefaultCheckoutTime(now)
            };

            _context.Add(checkout);

            var checkoutHistory = new CheckoutHistory
            {
                CheckedOut = now,
                LibraryAsset = item,
                LibraryCard = libraryCard
            };

            _context.Add(checkoutHistory);
            _context.SaveChanges();
        }

        public IEnumerable<Checkout> GetAll()
        {
            return _context.Checkouts;
        }

        public Checkout GetById(int Id)
        {
            return GetAll().FirstOrDefault(checkout => checkout.Id == Id);
        }

        public IEnumerable<CheckoutHistory> GetCheckoutHistory(int Id)
        {
            return _context.CheckoutHistories.Include(h => h.LibraryAsset).Include(h => h.LibraryCard).Where(h => h.LibraryAsset.Id == Id);
        }

        public string GetCurrentHoldCustomerName(int Id)
        {
            var hold = _context.Holds
                .Include(a => a.LibraryAsset)
                .Include(a => a.LibraryCard)
                .Where(v => v.Id == Id);

            var cardId = hold
                .Include(a => a.LibraryCard)
                .Select(a => a.LibraryCard.Id)
                .FirstOrDefault();

            var patron = _context.Customers
                .Include(p => p.LibraryCard)
                .First(p => p.LibraryCard.Id == cardId);

            return patron.FirstName + " " + patron.LastName;
        }

        public DateTime GetCurrentHoldPlaced(int Id)
        {
            return _context.Holds.Include(h => h.LibraryAsset).Include(h => h.LibraryCard).FirstOrDefault(h => h.Id == Id).HoldPlaced;
        }

        public IEnumerable<Hold> GetCurrentHolds(int Id)
        {
            return _context.Holds.Include(h => h.LibraryAsset).Where(h => h.LibraryAsset.Id == Id);
        }

        public Checkout GetLatestCheckout(int assetId)
        {
            return _context.Checkouts.Where(h => h.LibraryAsset.Id == assetId).OrderByDescending(h => h.Since).FirstOrDefault();
        }

        public void MarkFound(int assetId)
        {
            var item = _context.LibraryAssets
                .First(a => a.Id == assetId);

            _context.Update(item);
            item.Status = _context.Statuses.FirstOrDefault(a => a.Name == "Available");
            var now = DateTime.Now;

            // remove any existing checkouts on the item
            var checkout = _context.Checkouts
                .FirstOrDefault(a => a.LibraryAsset.Id == assetId);
            if (checkout != null) _context.Remove(checkout);

            // close any existing checkout history
            var history = _context.CheckoutHistories
                .FirstOrDefault(h =>
                    h.LibraryAsset.Id == assetId
                    && h.CheckedIn == null);
            if (history != null)
            {
                _context.Update(history);
                history.CheckedIn = now;
            }

            _context.SaveChanges();
        }

        public void MarkLost(int assetId)
        {
            var item = _context.LibraryAssets.FirstOrDefault(a => a.Id == assetId);
            _context.Update(item);
            item.Status = _context.Statuses.FirstOrDefault(status => status.Name == "Lost");
            _context.SaveChanges();
        }

        public void PlaceHold(int assetId, int LibraryCardId)
        {
            var now = DateTime.Now;

            var asset = _context.LibraryAssets
                .Include(a => a.Status)
                .First(a => a.Id == assetId);

            var card = _context.LibraryCards
                .First(a => a.Id == LibraryCardId);

            _context.Update(asset);

            if (asset.Status.Name == "Available")
                asset.Status = _context.Statuses.FirstOrDefault(a => a.Name == "On Hold");

            var hold = new Hold
            {
                HoldPlaced = now,
                LibraryAsset = asset,
                LibraryCard = card
            };

            _context.Add(hold);
            _context.SaveChanges();
        }

        public string GetCurrentCheckoutCustomer(int assetId)
        {
            var checkout = GetCheckoutByAssetId(assetId);
            if (GetCheckoutByAssetId(assetId) == null)
            {
                return "";
            }
            var cardId = checkout.LibraryCard.Id;
            var patron = _context.Customers.Include(p => p.LibraryCard).FirstOrDefault(p => p.LibraryCard.Id == assetId);
            return patron.FirstName + " " + patron.LastName;
        }

        private Checkout GetCheckoutByAssetId(int assetId)
        {
            return _context.Checkouts.Include(co => co.LibraryAsset).Include(co => co.LibraryCard).FirstOrDefault(co => co.LibraryAsset.Id == assetId);
        }
    }
}
