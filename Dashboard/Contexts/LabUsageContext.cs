using Dashboard.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Dashboard.Contexts
{
	public class LabUsageContext : DbContext
	{
		public DbSet<LibraryLabUsage> Usage { get; set; }

		public LabUsageContext()
            : base("name=Wildcat")
        {
            //
        }
	}
}