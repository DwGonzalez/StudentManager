using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Class> Class { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<StudentAttendance> StudentAttendance { get; set; }
        public DbSet<StudentClass> StudentClass { get; set; }
        public DbSet<TeacherClass> TeacherClass { get; set; }
    }
}