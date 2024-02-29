using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Student
{
    public class GetStudentClassInfo
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int ClassId { get; set; }
        public int Score { get; set; }
    }
}