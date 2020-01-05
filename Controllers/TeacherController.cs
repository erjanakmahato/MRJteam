using MRJTeam.Models;
using MRJTeam.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MRJTeam.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult ManageTeacher()
        {
            return View();
        }
        public JsonResult GetData()
        {
            using (Entities db = new Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<TeacherViewModel> lst = new List<TeacherViewModel>();
                var catList = db.tblTeachers.ToList();
                foreach (var item in catList)
                {
                    lst.Add(new TeacherViewModel() { TeacherId = item.TeacherId, TeacherName = item.TeacherName });
                }
                return Json(new { data = lst }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                using (Entities db = new Entities())
                {
                    ViewBag.Action = "New Teachers";
                    return View(new TeacherViewModel());
                }
            }
            else
            {
                using (Entities db = new Entities())
                {
                    TeacherViewModel sub = new TeacherViewModel();
                    var menu = db.tblTeachers.Where(x => x.TeacherId == id).FirstOrDefault();
                    sub.TeacherId = menu.TeacherId;
                    sub.TeacherName = menu.TeacherName;
                   

                    ViewBag.Action = "Edit Teacher";
                    return View(sub);
                }
            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(TeacherViewModel sm)
        {
            using (Entities db = new Entities())
            {
                if (sm.TeacherId == 0)
                {
                    tblTeacher tb = new tblTeacher();
                    tb.TeacherName = sm.TeacherName;
                  
                    db.tblTeachers.Add(tb);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    tblTeacher tbm = db.tblTeachers.Where(m => m.TeacherId == sm.TeacherId).FirstOrDefault();
                    tbm.TeacherName = sm.TeacherName;
                    //tbm.DepartmentName = sm.DepartmentName;
                    //tbm.StudentRollNo = sm.StudentRollNo;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }


        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (Entities db = new Entities())
            {
                tblTeacher sm = db.tblTeachers.Where(x => x.TeacherId == id).FirstOrDefault();
                db.tblTeachers.Remove(sm);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}