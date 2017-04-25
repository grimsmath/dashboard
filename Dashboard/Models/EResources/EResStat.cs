using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dashboard.Models
{
     [Table("EResStat")]
    public class EResStat
    {
		 [Key]
         public int StatID { get; set; }
		 public int DatabaseID { get; set; }
		 public int Month { get; set; }
         public int Year { get; set; }
         public int Searches { get; set; }
         public int Downloads { get; set; }
         public int LinkedTo { get; set; }
         public int LinkedFrom { get; set; }
    }
}