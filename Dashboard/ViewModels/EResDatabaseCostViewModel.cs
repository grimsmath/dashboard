using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Dashboard.ViewModels
{
    public class EResDatabaseCostViewModel
    {
        [DisplayName("Database")]
        public int DatabaseId { get; set; }

        [DisplayName("Database")]
        public string Database { get; set; }

        [DisplayName("Database Year")]
        public int Year { get; set; }

        [DisplayName("Vendor")]
        public string Vendor { get; set; }

        [DisplayName("Funding Source")]
        public string FundingSource { get; set; }

        [DisplayName("Database Cost")]
        public double Cost { get; set; }

        public string CostString
        {
            get
            {
                return this.Cost.ToString("C");
            }
        }
    }
}