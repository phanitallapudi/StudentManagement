using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StudentManagement.Models
{
    public class DbServicesContext : DbContext
    {
        public DbSet<Student> Std_Table { get; set; }
        public DbSet<StudentInfo> Std_TableInfo { get; set; }

    }
}