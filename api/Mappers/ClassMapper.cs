using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Class;
using api.Dtos.Student;
using api.Models;

namespace api.Mappers
{
    public static class ClassMapper
    {
        public static ClassDto ToClassDto(this Class classModel)
        {
            return new ClassDto
            {
                Id = classModel.Id,
                Name = classModel.Name
            };
        }

        public static Class ToClassFromCreateDto(this CreateClassRequestDto classModel)
        {
            return new Class
            {
                Name = classModel.Name,
            };
        }

        public static StudentClass ToStudentClassFromCreateDto(this AddStudentToClassRequestDto studentModel, int classId)
        {
            return new StudentClass
            {
                ClassId = classId,
                StudentId = studentModel.StudentId
            };
        }
    }
}