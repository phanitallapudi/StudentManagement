using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StudentManagement.Models
{
    public class AdminDbServicesContext : DbContext
    {
        public DbSet<Admin> Adm_Table { get; set; }
    }
}