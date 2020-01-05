using MRJTeam.Models;
using MRJTeam.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.Security;

namespace MRJTeam.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Login(LoginViewModel l, string ReturnUrl = "")
        {
            using (Entities db = new Entities())
            {
                var users = db.tblUsers.Where(a => a.Username == l.Username && a.Password == l.Password).FirstOrDefault();
                if (users != null)
                {
                    Session.Add("fullname",users.Fullname);
                    FormsAuthentication.SetAuthCookie(l.Username, l.RememberMe);
                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Dashboard", "Admin");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid User");
                }
            }
            return View();

        }
        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
