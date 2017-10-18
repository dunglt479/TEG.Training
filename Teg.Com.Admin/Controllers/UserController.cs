using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Teg.Com.Admin.Models;
using Teg.Com.Model;

namespace Teg.Com.Admin.Controllers
{
    public class UserController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = UserServices.SearchFor(x => loginViewModel.UserName.Equals(x.UserName.ToLower())
                                                       && x.Password.Equals(loginViewModel.Password));
                if (user.Any())
                {
                    FormsAuthentication.SetAuthCookie(loginViewModel.UserName, loginViewModel.RememberMe);

                    Session["USER_NAME"] = loginViewModel.UserName;

                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.ErrorMsg = "User name or Password incorrect. Please try again";
            return View();
        }
    }
}