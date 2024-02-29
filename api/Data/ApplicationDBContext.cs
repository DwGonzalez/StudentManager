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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Class>().HasData(
                new Class()
                {
                    Id = 1,
                    Name = "Matematicas"
                },
                 new Class()
                 {
                     Id = 2,
                     Name = "Lengua Española"
                 }
            );


            modelBuilder.Entity<Student>().HasData(
                new Student()
                {
                    Id = 1,
                    FirstName = "Leonel",
                    LastName = "Fernandez"
                },
                new Student()
                {
                    Id = 2,
                    FirstName = "Luis",
                    LastName = "Abinader"
                }
            );

            modelBuilder.Entity<StudentClass>().HasData(
                new StudentClass()
                {
                    Id = 1,
                    ClassId = 1,
                    StudentId = 1,
                    Score = 80
                },
                new StudentClass()
                {
                    Id = 2,
                    ClassId = 2,
                    StudentId = 1,
                    Score = 70
                },
                new StudentClass()
                {
                    Id = 3,
                    ClassId = 1,
                    StudentId = 2,
                    Score = 65
                },
                new StudentClass()
                {
                    Id = 4,
                    ClassId = 2,
                    StudentId = 2,
                    Score = 100
                }
            );

            modelBuilder.Entity<Attendance>().HasData(
                new Attendance()
                {
                    Id = 1,
                    CreatedDate = DateTime.Parse("02/28/2024"),
                    ClassId = 1,
                    StudentId = 1,
                    IsPresent = true
                },
                new Attendance()
                {
                    Id = 2,
                    CreatedDate = DateTime.Parse("02/28/2024"),
                    ClassId = 1,
                    StudentId = 2,
                    IsPresent = false
                }
            );
        }

        public DbSet<Class> Class { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<StudentClass> StudentClass { get; set; }
        public DbSet<TeacherClass> TeacherClass { get; set; }
    }
}