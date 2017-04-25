using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dashboard.Models
{
    [Table("EResDatabase")]
    public class EResDatabase
    {
		[Key]
        public int DatabaseID { get; set; }
        public string DatabaseName {get; set;}
		public int DatabaseType { get; set; }
		public decimal DatabaseCost { get; set; }
		public int FundingSource { get; set; }
		public int VendorID { get; set; }
    }
}