using BlockchainHOT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BlockchainHOT.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel login)
        {
            if(login.Password != "Password123")
            {
                return View();
            }
            SetupUserContext(login.UserName);
            return RedirectToAction("Index", "Home");
        }

        private void SetupUserContext(string username)
        {
            FormsAuthentication.SetAuthCookie(username, false);
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}