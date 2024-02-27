using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Student;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public StudentController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var students = _context.Student.ToList();

            return Ok(students);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var student = _context.Student.Find(id);

            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpPost]
        public IActionResult AddStudent([FromBody] CreateStudentRequestDto student)
        {
            var studentModel = student.ToStudentFromCreateDto();

            _context.Student.Add(studentModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = studentModel.Id }, studentModel.ToStudentDto());
        }


    }
}