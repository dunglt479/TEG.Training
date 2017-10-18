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
        public ActionResult Login(LoginViewModel loginViewModel)
        {

            var user = UserServices.SearchFor(x => x.UserName.ToUpper() == loginViewModel.UserName && x.Password == loginViewModel.Password);
            if (user.Count() > 0)
            {
                FormsAuthentication.SetAuthCookie(loginViewModel.UserName, loginViewModel.RememberMe);

                Session["USER_NAME"] = loginViewModel.UserName;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(String.Empty, "User Name or Password incorrect.");
                return View(loginViewModel);
            }

        }
    }
}