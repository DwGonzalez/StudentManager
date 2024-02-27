using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Student
{
    public class AddStudentToClassRequestDto
    {
        public int ClassId { get; set; }
        public int StudentId { get; set; }
    }
}