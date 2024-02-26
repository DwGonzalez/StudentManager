using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class StudentAttendance
    {
        public int Id { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public virtual AppUser? Student { get; set; }
        public int AttendanceId { get; set; }
        public virtual Attendance? Attendance { get; set; }
        public bool IsPresent { get; set; }
    }
}