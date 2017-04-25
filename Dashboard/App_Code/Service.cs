using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Authentication;

namespace Dashboard
{
    public class Service
    {
        public static string RenderActiveNav(string controlId, string compareId)
        {
            return (controlId.CompareTo(compareId) == 0) ? "active" : "inactive";
        }

        public static string RenderLoginLogout(string username, bool bLoggedIn)
        {
            return (bLoggedIn)
                ? "<li class=\"logout\"><a href=\"/Admin/Logout\">" + username + "&nbsp;LOGOUT</a></li>"
                : "<li class=\"logout\"><a href=\"/Admin/Login\">LOGIN</a></li>";
        }
    }
}