using Dashboard.Filters;
using Dashboard.Models;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace Dashboard.Controllers
{
	[Authorize]
	[InitializeSimpleMembership]
	public class AccountController : Controller
	{
		[AllowAnonymous]
		public ActionResult Login(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult Login(LoginModel model, string returnUrl)
		{
			if (ModelState.IsValid && Membership.ValidateUser(model.UserName, model.Password))
			{
				FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);

                // At this point we need to determine if the user is already in the system
                // if they aren't, then we need to add them to the UserProfile table
                // and then give them a default UserRole

				return RedirectToLocal(returnUrl);
			}
			// If we got this far, something failed, redisplay form
			ModelState.AddModelError("", "The user name or password provided is incorrect.");
			return View(model);
		}

        public ActionResult ShowUserRoles()
        {
            string[] roleNames = Roles.GetRolesForUser();

            return View(roleNames);
        }

		public ActionResult LogOff()
		{
			FormsAuthentication.SignOut();

			return RedirectToAction("Index", "Home");
		}

		private ActionResult RedirectToLocal(string returnUrl)
		{
			if (Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}
	}
}
