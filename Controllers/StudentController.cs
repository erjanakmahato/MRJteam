using MRJTeam.Models;
using MRJTeam.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Syncfusion.XlsIO;
using System.IO;

namespace MRJTeam.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult ManageStudent()
        {
            //Entities entities = new Entities();
            //return View(entities.tblStudents.ToList());
            return View();
        }



      





















        public JsonResult GetData()
        {
            using (Entities db = new Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<StudentViewModel> lst = new List<StudentViewModel>();
                var catList = db.tblStudents.ToList();
                foreach (var item in catList)
                {
                    lst.Add(new StudentViewModel() { StudentId = item.StudentId, StudentName = item.StudentName,StudentRollNo=item.StudentRollNo,DepartmentName=item.DepartmentName,Attendance=item.Attendance});
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
                    ViewBag.Action = "New Student";
                    return View(new StudentViewModel());
                }
            }
            else
            {
                using (Entities db = new Entities())
                {
                    StudentViewModel sub = new StudentViewModel();
                    var menu = db.tblStudents.Where(x => x.StudentId == id).FirstOrDefault();
                    sub.StudentId = menu.StudentId;
                    sub.StudentName = menu.StudentName;
                    sub.StudentRollNo = menu.StudentRollNo;
                    //sub.Time = menu.Time;
                    sub.DepartmentName = menu.DepartmentName;
                    sub.FingerId = menu.StudentId;
                    
                    ViewBag.Action = "Edit Student";
                    return View(sub);
                }
            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(StudentViewModel sm)
        {
            using (Entities db = new Entities())
            {
                if (sm.StudentId == 0)
                {
                    tblStudent tb = new tblStudent();
                    tb.StudentName = sm.StudentName;
                    tb.DepartmentName = sm.DepartmentName;
                    tb.StudentRollNo = sm.StudentRollNo;
                    db.tblStudents.Add(tb);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    tblStudent tbm = db.tblStudents.Where(m => m.StudentId == sm.StudentId).FirstOrDefault();
                    tbm.StudentName = sm.StudentName;
                    tbm.DepartmentName = sm.DepartmentName;
                    tbm.StudentRollNo = sm.StudentRollNo;
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
                tblStudent sm = db.tblStudents.Where(x => x.StudentId == id).FirstOrDefault();
                db.tblStudents.Remove(sm);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}