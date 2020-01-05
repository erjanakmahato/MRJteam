using MRJTeam.Models;
using MRJTeam.Models.ViewModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdvanceLayoutPage.Controllers
{
    public class UserController : Controller
    {
        // GET: User

        Entities _db = new Entities();
  public ActionResult Index()
        {
            List<UserViewModel> lst = new List<UserViewModel>();
            var Users = _db.tblUsers.ToList();
            foreach (tblUser item in Users)
            {
                lst.Add(new UserViewModel() { UserId = item.UserId, Username = item.Username, Password = item.Password, Fullname = item.Fullname });

            }

            return View(lst);
        }
        public ActionResult Create()
        {
            return View();
        }
       
    }
}