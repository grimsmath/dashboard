using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard.ViewModels
{
	public class EResDatabaseViewModel
	{
		/*	
		 * DatabaseID = r.DatabaseID,
		 * DatabaseName = r.DatabaseName,
		 * DatabaseType = r.DatabaseType,
		 * DatabaseCost = r.DatabaseCost,
		 * FundingSource = r.FundingSource,
		 * VendorID = r.VendorID,
		 * VendorName = v.VendorName
		 */

		public int DatabaseID { get; set; }
		public string DatabaseName { get; set; }
		public int DatabaseType { get; set; }
		public string DatabaseTypeName { get; set; }
		public decimal DatabaseCost { get; set; }
		public string DatabaseCostString { get; set; }
		public int FundingSource { get; set; }
		public string FundingSourceName { get; set; }
		public int VendorID { get; set; }
		public string VendorName { get; set; }
	}
}