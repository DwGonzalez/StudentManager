using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Class;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ClassRepository : IClassRepository
    {
        private readonly ApplicationDBContext _context;
        public ClassRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<bool> ClassExists(int id)
        {
            return await _context.Class.AnyAsync(c => c.Id == id);
        }

        public async Task<List<Class>> GetAllAsync()
        {
            return await _context.Class.ToListAsync();
        }
        public async Task<Class> CreateAsync(Class classModel)
        {
            await _context.Class.AddAsync(classModel);
            await _context.SaveChangesAsync();

            return classModel;
        }

        public async Task<Class?> DeleteAsync(int id)
        {
            var classModel = await _context.Class.FirstOrDefaultAsync(x => x.Id == id);

            if (classModel == null)
                return null;

            _context.Class.Remove(classModel);
            await _context.SaveChangesAsync();

            return classModel;
        }


        public async Task<Class?> GetByIdAsync(int id)
        {
            return await _context.Class.FindAsync(id);
        }

        public async Task<Class?> UpdateAsync(int id, UpdateClassRequestDto classDto)
        {
            var existingClass = await _context.Class.FirstOrDefaultAsync(x => x.Id == id);
            if (existingClass == null)
                return null;

            existingClass.Name = classDto.Name;

            await _context.SaveChangesAsync();

            return existingClass;
        }

        public async Task<List<StudentClass>> AddStudentsToClass(int classId, List<StudentClass> students)
        {
            await _context.StudentClass.AddRangeAsync(students);
            await _context.SaveChangesAsync();

            return students;
        }
    }
}