using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Attendance;
using api.Models;

namespace api.Mappers
{
    public static class AttendanceMapper
    {
        public static Attendance ToAttendanceFromDto(this AttendanceDto attendanceDto, int classId)
        {
            return new Attendance
            {
                CreatedDate = attendanceDto.CreatedDate,
                ClassId = classId,
                StudentId = attendanceDto.StudentId,
                IsPresent = attendanceDto.IsPresent,
            };
        }

        public static AttendanceDto ToDtoFromAttendance(this Attendance attendanceDto, int classId)
        {
            return new AttendanceDto
            {
                Id = attendanceDto.Id,
                CreatedDate = attendanceDto.CreatedDate,
                ClassId = classId,
                StudentId = attendanceDto.StudentId,
                IsPresent = attendanceDto.IsPresent,
            };
        }

        public static Attendance ToAttendanceFromCreateDto(this CreateAttendanceDto attendanceModel, int classId)
        {
            return new Attendance
            {
                CreatedDate = DateTime.Now,
                ClassId = classId,
                StudentId = attendanceModel.StudentId,
                IsPresent = attendanceModel.IsPresent
            };
        }

        public static AttendanceList ToAttendanceList(this Attendance attendanceDto, int classId)
        {
            return new AttendanceList
            {
                CreatedDate = attendanceDto.CreatedDate,
            };
        }
    }
}