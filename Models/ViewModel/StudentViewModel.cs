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
        public string ParentName { get; set; }
        [Display(Name = "Mobile Number:")]
        [Required(ErrorMessage = "Mobile Number is required.")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string ParentNumber { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        [Required]
        public string Time { get; set; }
        public string Attendance { get; set; }
        public int FingerId { get; set; }
        [DataType(DataType.Date)]
        public string LeaveTime { get; set; }
        [DataType(DataType.Date)]
        public string  ArrivalTime { get; set; }

    }
}