using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Class;
using api.Dtos.Student;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/class")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassRepository _repository;
        private readonly IStudentRepository _studentRepository;

        public ClassController(IClassRepository repository, IStudentRepository studentRepository)
        {
            _repository = repository;
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var classes = await _repository.GetAllAsync();

            return Ok(classes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var course = await _repository.GetByIdAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClassRequestDto classDto)
        {
            var classModel = classDto.ToClassFromCreateDto();

            await _repository.CreateAsync(classModel);

            return CreatedAtAction(nameof(GetById), new { id = classModel.Id }, classModel.ToClassDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateClassRequestDto updateDto)
        {
            var classModel = await _repository.UpdateAsync(id, updateDto);

            if (classModel == null)
            {
                return NotFound();
            }

            return Ok(classModel.ToClassDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var course = await _repository.DeleteAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        [Route("{id}/addToClass")]
        public async Task<IActionResult> AddStudentsToClass([FromRoute] int id, [FromBody] List<AddStudentToClassRequestDto> studentsDto)
        {
            if (!await _repository.ClassExists(id))
            {
                return BadRequest("Class does not exist");
            }

            var students = new List<StudentClass>();

            foreach (var st in studentsDto)
            {
                students.Add(st.ToStudentClassFromCreateDto(id));
            }

            await _repository.AddStudentsToClass(id, students);

            // await _context.StudentClass.AddRangeAsync(students);
            // await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("{id}/students")]
        public async Task<IActionResult> GetClassStudents([FromRoute] int id)
        {
            if (!await _repository.ClassExists(id))
            {
                return BadRequest("Class does not exist");
            }

            var students = await _studentRepository.GetStudentsFromClass(id);

            // var studentsDto = students.Select(s => s.ToStudentClassFromStudentDto());

            return Ok(students);
        }

        [HttpPut]
        [Route("{id}/assign-score")]
        public async Task<IActionResult> AssignStudentScore([FromRoute] int id, AssignScoreRequest request)
        {
            return Ok();
        }

        [HttpGet]
        [Route("{id}/get-attendance")]
        public async Task<IActionResult> GetAttendance([FromRoute] int id)
        {
            return Ok();
        }

        [HttpPost]
        [Route("{id}/create-attendance")]
        public async Task<IActionResult> CreateAttendance([FromRoute] int id)
        {
            return Ok();
        }
    }
}