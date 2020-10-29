using System;
using System.Collections.Generic;
using System.Text;
using FYP1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FYP1.Models
{
   
    public class ApplicationDbContext :IdentityDbContext
    { 

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
               : base(options)
        {
        }
        public DbSet<appDatabase> appDatabases { get; set; }
        public DbSet<dbTables> dbTables { get; set; }
        public DbSet<userApplication> userApplications { get; set; }

        public DbSet<Feedback> feedback { get; set; }

        public DbSet<WebAdmin> webAdmin { get; set; }

        public DbSet<Query> query { get; set; }



    }

}
