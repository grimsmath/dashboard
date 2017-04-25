using Dashboard.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Dashboard.Contexts
{
	public class PatronCounterContext : DbContext
	{
		public DbSet<PatronCounter> Patrons { get; set; }

		public PatronCounterContext()
			: base("name=Libcounter")
		{
			
		}
	}
}