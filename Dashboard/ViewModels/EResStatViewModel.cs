using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard.ViewModels
{
	public class EResStatViewModel
	{
		public int StatID { get; set; }
		public int DatabaseID { get; set; }
		public string DatabaseName { get; set; }
		public int Month { get; set; }
		public int Year { get; set; }
		public int Searches { get; set; }
		public int Downloads { get; set; }
		public int LinkedTo { get; set; }
		public int LinkedFrom { get; set; }
		public decimal CostPerUse { get; set; }
		public decimal CostPerLinkFrom { get; set; }
		public long? Percentage { get; set; }
	}
}