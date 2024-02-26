using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class TeacherClass
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public virtual Class? Class { get; set; }
        public string TeacherId { get; set; } = string.Empty;
        public virtual AppUser? Teacher { get; set; }
    }
}