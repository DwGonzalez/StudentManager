using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Attendance;
using api.Models;

namespace api.Interfaces
{
    public interface IAttendanceRepository
    {
        Task<List<Attendance>> GetAttendanceDetailsAsync(int classId);
        Task<List<AttendanceList>> GetAttendanceAsync(int classId);
        Task<List<Attendance>> CreateAttendanceAsync(int classId, List<Attendance> attendances);
    }
}