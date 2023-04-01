using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentManagement.Models
{
    public class TableViewModel
    {
        public List<Student> Student_List_Ref { get; set; }
        public List<StudentInfo> Student_Info_Ref { get; set; }
    }
}