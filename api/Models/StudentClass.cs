using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Models
{
    public class StudentClass
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public virtual Class? Class { get; set; }
        public int StudentId { get; set; }
        public virtual Student? Student { get; set; }
        public int Score { get; set; }
    }
}