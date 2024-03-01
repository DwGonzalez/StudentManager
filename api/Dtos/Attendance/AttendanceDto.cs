using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Attendance
{
    public class AttendanceDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now.Date;
        public int ClassId { get; set; }
        public int StudentId { get; set; }
        public bool IsPresent { get; set; }
    }
}