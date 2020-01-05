using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MRJTeam.Models.ViewModel
{
    public class StudentViewModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        [Required]
        public Nullable<int> StudentRollNo { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        [Required]
        public string Time { get; set; }
        public string Attendance { get; set; }
        public int FingerId { get; set; }
        public DateTime LeaveTime { get; set; }
        public DateTime  ArrivalTime { get; set; }

    }
}