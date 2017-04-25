using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dashboard.Models.Tickler
{
    [Table("TicklerSubLibraries")]
    public class TicklerSubLibrary
    {
		[Key]
        public int id { get; set; }
        public string Title { get; set; }
        public String LegacyCode { get; set; }
        public int StatusID { get; set; }
    }
}