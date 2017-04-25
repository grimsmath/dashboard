using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dashboard.Models.EResources
{
    [Table("EResDatabaseYear")]
    public class EResDatabaseCost
    {
        public int id { get; set; }
        public int DatabaseId { get; set; }
        public int Year { get; set; }
        public double Cost { get; set; }
    }
}