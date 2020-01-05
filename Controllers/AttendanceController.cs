using MRJTeam.Models;
using MRJTeam.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MRJTeam.Controllers
{
    public class AttendanceController : Controller
    {
        // GET: Attendance
        Entities db = new Entities();
        public ActionResult Index(int id)
        {
            List<int> Id = new List<int>();
            Id.Add(id);




            foreach (var D in Id)
            {
                StudentViewModel sub = new StudentViewModel();
                var menu = db.tblStudents.Where(x => x.StudentId == id).FirstOrDefault();
               // int a = 1;
                if (menu != null)
                {
                   // sub.StudentId = menu.StudentId;
                  //  sub.Attendance = menu.Attendance;



                    if (menu.ArrivalTime == null)
                    {
                        menu.ArrivalTime = DateTime.Now;
                        menu.Attendance = "present";
                        db.SaveChanges();
                        return View();
                        
                    }
                    else if (menu.LeaveTime == null)
                    {
                        menu.LeaveTime = DateTime.Now;
                        db.SaveChanges();
                        return View();
                        

                    }
                    else
                    {
                        return View();
                    }
                }
            }   

                return View();
               
            }
        public ActionResult Value()
        {
            ViewBag.id = 1;
            return View();
        }
        
    }
}