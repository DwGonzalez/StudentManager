using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Student;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDBContext _context;
        public StudentRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<bool> StudentExists(int id)
        {
            return await _context.Student.AnyAsync(s => s.Id == id);
        }
        public async Task<List<Student>> GetAllAsync()
        {
            return await _context.Student.ToListAsync();
        }
        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _context.Student.FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task<Student> CreateAsync(Student studentModel)
        {
            await _context.Student.AddAsync(studentModel);
            await _context.SaveChangesAsync();

            return studentModel;
        }

        public async Task<Student?> UpdateAsync(int id, UpdateStudentRequestDto studentDto)
        {
            var existingStudent = await _context.Student.FirstOrDefaultAsync(x => x.Id == id);
            if (existingStudent == null)
                return null;

            existingStudent.FirstName = studentDto.FirstName;
            existingStudent.LastName = studentDto.LastName;

            await _context.SaveChangesAsync();

            return existingStudent;
        }

        public async Task<Student?> DeleteAsync(int id)
        {
            var studentModel = await _context.Student.FirstOrDefaultAsync(x => x.Id == id);

            if (studentModel == null)
                return null;

            _context.Student.Remove(studentModel);
            await _context.SaveChangesAsync();

            return studentModel;
        }

        public async Task<List<GetStudentClassInfo>> GetStudentsFromClass(int classId)
        {
            var students = await _context.StudentClass.Select(s =>
                new GetStudentClassInfo
                {
                    Id = s.Id,
                    StudentId = s.StudentId,
                    FirstName = s.Student.FirstName,
                    LastName = s.Student.LastName,
                    ClassId = s.ClassId,
                    Score = s.Score
                }
            ).Where(s => s.ClassId == classId).ToListAsync();

            return students;
        }

        public async Task<bool> AssignScore(int classId, AssignScoreRequest request)
        {
            var score = await _context.StudentClass.FirstOrDefaultAsync(sc => sc.ClassId == classId && sc.StudentId == request.StudentId);

            if (score == null)
                return false;

            score.Score = request.Score;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Student>> GetStudentsNotInClass(int classId)
        {
            var students = await _context.Student.Where(st => !_context.StudentClass.Any(sc => sc.ClassId == classId && sc.StudentId == st.Id)).Select(s => new
            Student
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName
            }).ToListAsync();

            return students;
        }

        public async Task<StudentClass?> RemoveStudentFromClass(int classId, int studentId)
        {
            var studentInClass = await _context.StudentClass.FirstOrDefaultAsync(sc => sc.ClassId == classId && sc.StudentId == studentId);

            if (studentInClass == null)
                return null;

            _context.StudentClass.Remove(studentInClass);
            await _context.SaveChangesAsync();

            return studentInClass;
        }

    }
}