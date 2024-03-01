using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Attendance;
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
        private readonly IAttendanceRepository _attendanceRepository;

        public ClassController(IClassRepository repository, IStudentRepository studentRepository, IAttendanceRepository attendanceRepository)
        {
            _repository = repository;
            _studentRepository = studentRepository;
            _attendanceRepository = attendanceRepository;
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

            return Ok(students);
        }

        [HttpGet]
        [Route("{id}/students-not-in-class")]
        public async Task<IActionResult> GetStudentsNotInClass([FromRoute] int id)
        {
            if (!await _repository.ClassExists(id))
            {
                return BadRequest("Class does not exist");
            }

            var students = await _studentRepository.GetStudentsNotInClass(id);

            return Ok(students);
        }

        [HttpDelete]
        [Route("{id}/student-from-class")]
        public async Task<IActionResult> DeleteStudentFromClass([FromRoute] int id, int studentId)
        {
            if (!await _repository.ClassExists(id))
            {
                return BadRequest("Class does not exist");
            }

            if (!await _studentRepository.StudentExists(studentId))
            {
                return BadRequest("Student does not exist");
            }

            var student = await _studentRepository.RemoveStudentFromClass(id, studentId);

            if (student == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut]
        [Route("{id}/assign-score")]
        public async Task<IActionResult> AssignStudentScore([FromRoute] int id, AssignScoreRequest request)
        {
            if (!await _repository.ClassExists(id))
            {
                return BadRequest("Class does not exist");
            }

            var score = await _studentRepository.AssignScore(id, request);

            return Ok();
        }

        [HttpGet]
        [Route("{id}/get-attendance-details")]
        public async Task<IActionResult> GetAttendance([FromRoute] int id)
        {
            if (!await _repository.ClassExists(id))
            {
                return BadRequest("Class does not exist");
            }

            var attendance = await _attendanceRepository.GetAttendanceDetailsAsync(id);

            return Ok(attendance.Select(a => a.ToDtoFromAttendance(id)).ToList());
        }

        [HttpGet]
        [Route("{id}/get-attendance-list")]
        public async Task<IActionResult> GetAttendanceList([FromRoute] int id)
        {
            if (!await _repository.ClassExists(id))
            {
                return BadRequest("Class does not exist");
            }

            var attendance = await _attendanceRepository.GetAttendanceAsync(id);

            return Ok(attendance.Distinct());
        }

        [HttpPost]
        [Route("{id}/create-attendance")]
        public async Task<IActionResult> CreateAttendance([FromRoute] int id, [FromBody] List<CreateAttendanceDto> request)
        {
            if (!await _repository.ClassExists(id))
            {
                return BadRequest("Class does not exist");
            }

            var attendanceList = new List<Attendance>();

            foreach (var at in request)
            {
                attendanceList.Add(at.ToAttendanceFromCreateDto(id));
            }

            var attendance = await _attendanceRepository.CreateAttendanceAsync(id, attendanceList);

            return Ok(attendance.Select(a => a.ToDtoFromAttendance(id)).ToList());
        }
    }
}