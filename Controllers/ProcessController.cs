using MRJTeam.Models;
using MRJTeam.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MRJTeam.Controllers
{
    public class ProcessController : Controller
    {
        // GET: Process
        Entities db = new Entities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(long id)
        {
            using (Entities db = new Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<StudentViewModel> lst = new List<StudentViewModel>();
                var catList = db.tblStudents.Where(x => x.ParentNumber == id.ToString()).ToList();

                foreach (var item in catList)
                {
                    lst.Add(new StudentViewModel() { StudentId = item.StudentId, StudentName = item.StudentName, StudentRollNo = item.StudentRollNo, ParentName = item.ParentName, ParentNumber = item.ParentNumber, DepartmentName = item.DepartmentName, Attendance = item.Attendance });
                }
                return Json(new { data = lst }, JsonRequestBehavior.AllowGet);
            }

            
        }

        public JsonResult GetData(long id)
        {
            using (Entities db = new Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<StudentViewModel> lst = new List<StudentViewModel>();
                var catList = db.tblStudents.Where(x => x.ParentNumber == id.ToString()).ToList();

                foreach (var item in catList)
                {
                    lst.Add(new StudentViewModel() { StudentId = item.StudentId, StudentName = item.StudentName, StudentRollNo = item.StudentRollNo, ParentName = item.ParentName, ParentNumber = item.ParentNumber, DepartmentName = item.DepartmentName, Attendance = item.Attendance });
                }
                return Json(new { data = lst }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}