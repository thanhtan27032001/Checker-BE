using GadenCheckIn.API.Common.Exceptions;
using GadenCheckIn.API.Data;
using GadenCheckIn.API.Dtos.Attendence;
using GadenCheckIn.API.Entities;
using GadenCheckIn.API.Mappers;
using Microsoft.EntityFrameworkCore;

namespace GadenCheckIn.API.Services;

public class AttendanceService(GadenCheckInDbContext db, IWorkScheduleService workScheduleService) : IAttendanceService
{
    private static readonly TimeZoneInfo VietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Ho_Chi_Minh");
    private static readonly Int16 AllowLateMinutes = 15;
    public async Task<AttendanceResponseDto> CheckIn(CheckInDto checkInDto)
    {
        var employeeExists = await db.Employees.AnyAsync(e => e.Id == checkInDto.EmployeeId);
        if (!employeeExists)
        {
            throw new NotFoundException($"No employee found with id = {checkInDto.EmployeeId}");
        }
        
        var hasOpenSession = await db.AttendanceRecords
            .AnyAsync(record => record.EmployeeId == checkInDto.EmployeeId && record.CheckOutTime == null);
        if (hasOpenSession)
        {
            throw new BusinessRuleException("You have already opened a attendance record for this employee");
        }

        var status = await DetermineStatusAsync(checkInDto.EmployeeId, DateTimeOffset.UtcNow);
        var record = new AttendanceRecord
        {
            EmployeeId = checkInDto.EmployeeId,
            CheckInTime = DateTimeOffset.UtcNow,
            CheckInMethod = checkInDto.Method,
            Status = status,
        };
            
        db.Add(record);
        await db.SaveChangesAsync();
        
        // reload attendance record
        await db.Entry(record).Reference(a => a.Employee).LoadAsync();
        
        return record.ToResponseDto();
    }

    public async Task<AttendanceResponseDto> CheckOut(Guid employeeId)
    {
        // check exist attendance with checkout = null
        var record = await db.AttendanceRecords
            .Include(a => a.Employee)
            .Where(a => a.EmployeeId == employeeId && a.CheckOutTime == null)
            .OrderByDescending(a => a.CheckInTime)
            .FirstOrDefaultAsync();
        if (record == null)
        {
            throw new NotFoundException(message: "Attendance record not found");
        }
        
        // check out
        record.CheckOutTime = DateTimeOffset.UtcNow;
        await db.SaveChangesAsync();

        // reload record
        return record.ToResponseDto();
    }

    public async Task<List<AttendanceResponseDto>> GetAll()
    {
        var result = await db.AttendanceRecords
            .Include(a => a.Employee)
            .Select(a => a.ToResponseDto())
            .ToListAsync();
        return result;
    }

    public async Task Delete(Guid id)
    {
        var record = await db.AttendanceRecords
            .FindAsync(id);
        if (record == null)
        {
            throw new NotFoundException($"No record found with id = {id}");
        }
        db.AttendanceRecords.Remove(record);
        await db.SaveChangesAsync();
    }

    private async Task<AttendanceStatus> DetermineStatusAsync(Guid employeeId, DateTimeOffset checkInTimeUtc)
    {
        var checkInLocal = TimeZoneInfo.ConvertTime(checkInTimeUtc, VietnamTimeZone);
        var employeeWorkSchedule = await workScheduleService.GetApplicableScheduleAsync(employeeId, checkInLocal.DayOfWeek);
        if (employeeWorkSchedule == null)
        {
            return AttendanceStatus.OnTime;
        }

        var graceLimit = employeeWorkSchedule.StartTime.AddMinutes(AllowLateMinutes);
        var checkInTime = TimeOnly.FromDateTime(checkInLocal.DateTime);
        
        return checkInTime > graceLimit ? AttendanceStatus.Late :  AttendanceStatus.OnTime;
    }

    // private async Task<AttendanceStatus> DetermineCheckOutStatusAsync()
    // {
    //     
    // }
}