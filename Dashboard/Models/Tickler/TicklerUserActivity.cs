using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dashboard.Models.Tickler
{
    [Table("TicklerUserActivity")]
    public class TicklerUserActivity
    {
		[Key]
        public int id { get; set; }
        public int UserID { get; set; }
        public int ActivityTypeID { get; set; }
    }
}