using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Student
{
    public class AssignScoreRequest
    {
        public int StudentId { get; set; }
        public int Score { get; set; }
    }
}