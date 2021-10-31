using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lib_Management.Models.Catalogs
{
    public class AssetIndexModel
    {
        public IEnumerable<AssetIndexListingModels> Assets { get; set; }
    }
}
