using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Attendance;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDBContext _context;
        public AttendanceRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Attendance>> CreateAttendanceAsync(int classId, List<Attendance> attendances)
        {
            await _context.Attendance.AddRangeAsync(attendances);
            await _context.SaveChangesAsync();

            return attendances;
        }

        public async Task<List<Attendance>> GetAttendanceDetailsAsync(int classId)
        {
            var attendance = await _context.Attendance.Where(a => a.ClassId == classId).ToListAsync();

            return attendance;
        }

        public async Task<List<AttendanceList>> GetAttendanceAsync(int classId)
        {
            var attendance = await _context.Attendance.Where(a => a.ClassId == classId).ToListAsync();
            var attendance2 = await _context.Attendance.Where(a => a.ClassId == classId).Select(x => new AttendanceList()
            {
                CreatedDate = x.CreatedDate
            }).Distinct().ToListAsync();

            return attendance2;
        }

    }
}