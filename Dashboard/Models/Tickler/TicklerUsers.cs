using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dashboard.Models.Tickler
{
    [Table("TicklerUsers")]
    public class TicklerUser
    {
		[Key]
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public String Password { get; set; }
        public int StatusID { get; set; }
        public int RoleID { get; set; }
    }
}