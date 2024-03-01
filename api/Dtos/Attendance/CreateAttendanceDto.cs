using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Attendance
{
    public class CreateAttendanceDto
    {
        public int StudentId { get; set; }
        public bool IsPresent { get; set; } = false;
    }
}