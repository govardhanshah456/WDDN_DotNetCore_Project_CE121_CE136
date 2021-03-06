using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lib_Management_Data.Models
{
    public abstract class LibraryAsset
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int Year { get; set; } // Just store as an int for BC

        [Required]
        public Status Status { get; set; }

        [Required]
        [Display(Name = "Cost of Replacement")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; }
        public string ImageUrl { get; set; }
        public int NumberOfCopies { get; set; }
        public string AssetType { get; set; }
        public virtual LibraryBranch Location { get; set; }
    }
}
