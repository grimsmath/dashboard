using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Models
{
    [Table("LibraryLabUsage")]
    public class LibraryLabUsage
    {
        public long ID { get; set; }
        public string Prefix { get; set; }
        public string Decal { get; set; }
        public string Userid { get; set; }
        public DateTime LDate { get; set; }
        public DateTime LDate2 { get; set; }
        public string Model { get; set; }
        public int Lab { get; set; }
        public long? Other { get; set; }
    }
}