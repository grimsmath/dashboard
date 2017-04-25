using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dashboard.Models
{
    [Table("EResVendor")]
    public class EResVendor
    {
		[Key]
        public int VendorID { get; set; }
        public string VendorName { get; set; }
    }
}