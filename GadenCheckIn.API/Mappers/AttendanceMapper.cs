using GadenCheckIn.API.Dtos.Attendence;
using GadenCheckIn.API.Entities;

namespace GadenCheckIn.API.Mappers;

public static class AttendanceMapper
{
    public static AttendanceResponseDto ToResponseDto(this AttendanceRecord entity)
    {
        double? workedHours = (entity.CheckInTime != null && entity.CheckOutTime != null) 
            ? Math.Round((entity.CheckOutTime.Value - entity.CheckInTime.Value).TotalHours, 2)
            : null;
        return new AttendanceResponseDto
        {
            Id = entity.Id,
            EmployeeId = entity.EmployeeId,
            EmployeeName = entity.Employee.FullName,
            CheckInMethod = entity.CheckInMethod,
            Status = entity.Status,
            WorkedHours = workedHours,
            CheckInTime = entity.CheckInTime,
            CheckOutTime = entity.CheckOutTime
        };
    }
}