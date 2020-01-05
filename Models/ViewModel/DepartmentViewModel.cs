using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MRJTeam.Models.ViewModel
{
    public class DepartmentViewModel
    {
        public int DepartmentId { get; set; }
        public Nullable<int> ReportId { get; set; }
        public Nullable<int> TeacherId { get; set; }
        public string DepartmentName { get; set; }


        public virtual tblReport tblReport { get; set; }
        public virtual tblReport tblReport1 { get; set; }
        public virtual tblTeacher tblTeacher { get; set; }
        public virtual tblTeacher tblTeacher1 { get; set; }
    }
}