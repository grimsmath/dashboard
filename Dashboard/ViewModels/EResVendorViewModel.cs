using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dashboard.ViewModels
{
	public class EResVendorViewModel
	{
		public int VendorID { get; set; }

		[Required]
		public string VendorName { get; set; }
	}
}