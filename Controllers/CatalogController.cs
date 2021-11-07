using Lib_Management.Models.Catalogs;
using Lib_Management.Models.CheckOutModels;
using Lib_Management_Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lib_Management.Controllers
{
    public class CatalogController: Controller
    {
        private ILibraryAsset _assetsService;
        private IChekout _checkoutsService;
        public CatalogController(ILibraryAsset assets,IChekout checkoutsService)
        {
            _assetsService = assets;
            _checkoutsService = checkoutsService;
        }
        public IActionResult Index()
        {
            var assetModels = _assetsService.GetAll();
            var listingResults = assetModels.Select(
                    result => new AssetIndexListingModels
                    {
                        Id = result.Id,
                        ImageURL = result.ImageUrl,
                        AuthorOrDirector = _assetsService.GetAuthorOrDirector(result.Id),
                        DeweyCallNumber = _assetsService.GetDeweyIndex(result.Id),
                        Title = result.Title,
                        Type = _assetsService.GetType(result.Id)
                    });
            var model = new AssetIndexModel()
            {
                Assets = listingResults
            };
            return View(model);
        }
        public IActionResult Detail(int id)
        {
            var asset = _assetsService.GetByID(id);

            var currentHolds = _checkoutsService.GetCurrentHolds(id).Select(a => new AssetHoldModel
            {
                HoldPlaced = _checkoutsService.GetCurrentHoldPlaced(a.Id).ToString("d"),
                CustomerName = _checkoutsService.GetCurrentHoldCustomerName(a.Id)
            });

            var model = new AssetDetailModel
            {
                AssetId = id,
                Title = asset.Title,
                Type = _assetsService.GetType(id),
                Year = asset.Year,
                Cost = asset.Cost,
                Status = asset.Status.Name,
                ImageURL = asset.ImageUrl,
                AuthorOrDirector = _assetsService.GetAuthorOrDirector(id),
                CurrentLocation = _assetsService.GetCurrentLocation(id)?.Name,
                DeweyCallNumber = _assetsService.GetDeweyIndex(id),
                CheckoutHistory = _checkoutsService.GetCheckoutHistory(id),
                ISBN = _assetsService.GetISBN(id),
                LatestCheckout = _checkoutsService.GetLatestCheckout(id),
                CurrentHolds = currentHolds,
                CustomerName = _checkoutsService.GetCurrentCheckoutCustomer(id)
            };

            return View(model);
        }
        public IActionResult Checkout(int id)
        {
            var asset = _assetsService.GetByID(id);

            var model = new CheckOutModel
            {
                AssetId = id,
                ImageUrl = asset.ImageUrl,
                Title = asset.Title,
                LibraryCardId = "",
                IsCheckedOut = _checkoutsService.IsCheckedOut(id)
            };
            return View(model);
        }

        public IActionResult Hold(int id)
        {
            var asset = _assetsService.GetByID(id);

            var model = new CheckOutModel
            {
                AssetId = id,
                ImageUrl = asset.ImageUrl,
                Title = asset.Title,
                LibraryCardId = "",
                HoldCount = _checkoutsService.GetCurrentHolds(id).Count()
            };
            return View(model);
        }

        public IActionResult CheckIn(int id)
        {
            _checkoutsService.CheckInItem(id);
            return RedirectToAction("Detail", new { id });
        }

        public IActionResult MarkLost(int id)
        {
            _checkoutsService.MarkLost(id);
            return RedirectToAction("Detail", new { id });
        }

        public IActionResult MarkFound(int id)
        {
            _checkoutsService.MarkFound(id);
            return RedirectToAction("Detail", new { id });
        }

        [HttpPost]
        public IActionResult PlaceCheckout(int assetId, int libraryCardId)
        {
            _checkoutsService.CheckOutItem(assetId, libraryCardId);
            return RedirectToAction("Detail", new { id = assetId });
        }

        [HttpPost]
        public IActionResult PlaceHold(int assetId, int libraryCardId)
        {
            _checkoutsService.PlaceHold(assetId, libraryCardId);
            return RedirectToAction("Detail", new { id = assetId });
        }
    }
}
