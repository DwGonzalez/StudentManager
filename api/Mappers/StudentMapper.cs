using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using api.Dtos.Student;
using api.Models;

namespace api.Mappers
{
    public static class StudentMapper
    {
        public static StudentDto ToStudentDto(this Student studentModel)
        {
            return new StudentDto
            {
                Id = studentModel.Id,
                FirstName = studentModel.FirstName,
                LastName = studentModel.LastName
            };
        }

        public static Student ToStudentFromCreateDto(this CreateStudentRequestDto studentModel)
        {
            return new Student
            {
                FirstName = studentModel.FirstName,
                LastName = studentModel.LastName
            };
        }

        public static GetStudentClassInfo ToStudentClassFromStudentDto(this StudentClass studentModel)
        {
            return new GetStudentClassInfo
            {
                Id = studentModel.Id,
                FirstName = studentModel.Student.FirstName,
                LastName = studentModel.Student.LastName,
                ClassId = studentModel.ClassId,
                StudentId = studentModel.StudentId,
                Score = studentModel.Score
            };
        }

    }
}