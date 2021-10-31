using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lib_Management_Data.Models
{
    public class Video : LibraryAsset
    {
        [Required]
        public string Director { get; set; }
    }
}
