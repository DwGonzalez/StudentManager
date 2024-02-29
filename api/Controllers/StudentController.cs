using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Student;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStudentRepository _repository;
        public StudentController(IStudentRepository repository, ApplicationDBContext context)
        {
            _context = context;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _repository.GetAllAsync();

            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var student = await _repository.GetByIdAsync(id);

            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] CreateStudentRequestDto studentDto)
        {
            var studentModel = studentDto.ToStudentFromCreateDto();

            await _repository.CreateAsync(studentModel);

            return CreatedAtAction(nameof(GetById), new { id = studentModel.Id }, studentModel.ToStudentDto());
        }

        [HttpPut]
        [Route("{studentId}")]
        public async Task<IActionResult> UpdateStudent(int studentId, [FromBody] UpdateStudentRequestDto studentDto)
        {
            var studentModel = await _repository.UpdateAsync(studentId, studentDto);

            if (studentModel == null)
            {
                return NotFound();
            }

            return Ok(studentModel.ToStudentDto());
        }


    }
}