using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Class;
using api.Dtos.Student;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/class")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public ClassController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var classes = _context.Class.ToList();

            return Ok(classes);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var course = _context.Class.Find(id);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateClassRequestDto classDto)
        {
            var classModel = classDto.ToClassFromCreateDto();

            _context.Class.Add(classModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = classModel.Id }, classModel.ToClassDto());
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateClassRequestDto updateDto)
        {
            var classModel = _context.Class.FirstOrDefault(x => x.Id == id);

            if (classModel == null)
            {
                return NotFound();
            }

            classModel.Name = updateDto.Name;

            _context.SaveChanges();

            return Ok(classModel.ToClassDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var course = _context.Class.Find(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Class.Remove(course);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPost]
        [Route("addStudentsToClass")]
        public IActionResult AddStudentsToClass([FromBody] List<AddStudentToClassRequestDto> studentsDto)
        {
            var students = new List<StudentClass>();

            foreach (var st in studentsDto)
            {
                students.Add(st.ToStudentClassFromCreateDto());
            }

            _context.StudentClass.AddRange(students);
            _context.SaveChanges();

            return Ok();
        }
    }
}