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
        public virtual Class? Class { get; set; }
        public int StudentId { get; set; }
        public virtual Student? Student { get; set; }
        public bool IsPresent { get; set; }
    }
}