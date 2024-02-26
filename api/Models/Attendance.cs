using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ClassId { get; set; }
        public Class? Class { get; set; }
    }
}