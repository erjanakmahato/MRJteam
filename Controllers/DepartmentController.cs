using MRJTeam.Models;
using MRJTeam.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MRJTeam.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        public ActionResult ManageDepartment()
        {
            return View();
        }

        public JsonResult GetData()
        {
            using (Entities db = new Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<DepartmentViewModel> lst = new List<DepartmentViewModel>();
                var catList = db.tblDepartments.ToList();
                foreach (var item in catList)
                {
                    lst.Add(new DepartmentViewModel() {  DepartmentId=item.DepartmentId,ReportId=item.ReportId,TeacherId=item.TeacherId,DepartmentName = item.DepartmentName});
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
                    ViewBag.Students = db.tblStudents.ToList();
                    ViewBag.Action = "Add New Department";
                    return View(new DepartmentViewModel());
                }
            }
            else
            {
                using (Entities db = new Entities())
                {
                    ViewBag.Action = "Edit Department";
                    ViewBag.Students = db.tblStudents.ToList();
                    tblDepartment item = db.tblDepartments.Where(i => i.DepartmentId == id).FirstOrDefault();
                    DepartmentViewModel itemvm = new DepartmentViewModel();
                    itemvm.DepartmentId = item.DepartmentId;
                  //  itemvm.StudentId = Convert.ToInt32(item.StudentId);
                    itemvm.DepartmentName = item.DepartmentName;
                    itemvm.TeacherId = item.TeacherId;
                    itemvm.ReportId = item.ReportId;
                   

                    return View(itemvm);
                }
            }
        }

        [HttpPost]

        public ActionResult AddOrEdit(DepartmentViewModel dvm)
        {
            using (Entities db = new Entities())
            {
                if (dvm.DepartmentId == 0)
                {
                    tblDepartment itm = new tblDepartment();

                   // itm.StudentId = Convert.ToInt32(dvm.StudentId);
                    itm.DepartmentName =dvm.DepartmentName;
                    itm.ReportId= dvm.ReportId;
                    itm.TeacherId = dvm.TeacherId;
                    
                    //HttpPostedFileBase fup = Request.Files["Photo"];
                    //if (fup != null)
                    //{
                    //    if (fup.FileName != "")
                    //    {
                    //        fup.SaveAs(Server.MapPath("~/ProductImages/" + fup.FileName));
                    //        itm.Photo = fup.FileName;
                    //    }
                    //}



                    db.tblDepartments.Add(itm);
                    db.SaveChanges();
                    ViewBag.Message = "Updated Successfully";
                }
                else
                {
                    tblDepartment itm = db.tblDepartments.Where(i => i.DepartmentId == dvm.DepartmentId).FirstOrDefault();
                  // itm.StudentId = Convert.ToInt32(dvm.StudentId);
                    itm.DepartmentName = dvm.DepartmentName;
                    itm.ReportId = dvm.ReportId;
                    itm.TeacherId = dvm.TeacherId;
                    
                    //HttpPostedFileBase fup = Request.Files["SmallImage"];
                    //if (fup != null)
                    //{
                    //    if (fup.FileName != "")
                    //    {
                    //        fup.SaveAs(Server.MapPath("~/ProductImages/" + fup.FileName));
                    //        itm.Photo = fup.FileName;
                    //    }
                    //}



                    db.SaveChanges();
                    ViewBag.Message = "Updated Successfully";

                }
                ViewBag.Students = db.tblStudents.ToList();
                return View(new DepartmentViewModel());

            }


        }

        [HttpPost]

        public ActionResult Delete(int id)
        {
            using (Entities db = new Entities())
            {
                tblDepartment sm = db.tblDepartments.Where(x => x.DepartmentId == id).FirstOrDefault();
                db.tblDepartments.Remove(sm);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}