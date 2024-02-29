using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Student;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Interfaces
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int id);
        Task<Student> CreateAsync(Student studentModel);
        Task<Student?> UpdateAsync(int id, UpdateStudentRequestDto classDto);
        Task<Student?> DeleteAsync(int id);
        Task<bool> StudentExists(int id);
        Task<List<GetStudentClassInfo>> GetStudentsFromClass(int classId);
    }
}