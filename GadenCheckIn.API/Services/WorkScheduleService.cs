using GadenCheckIn.API.Common.Exceptions;
using GadenCheckIn.API.Data;
using GadenCheckIn.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace GadenCheckIn.API.Services;

public class WorkScheduleService(GadenCheckInDbContext db) : IWorkScheduleService
{
    public async Task<WorkSchedule?> GetApplicableScheduleAsync(Guid employeeId, DayOfWeek dayOfWeek)
    {
        // check employee exist
        var employee = await db.Employees.FindAsync(employeeId);
        if (employee == null)
        {
            throw new NotFoundException($"No employee found with id {employeeId}");
        }

        // get employee individual schedule
        var dayCode = ToDayCode(dayOfWeek);
        var employeeSchedule = await db.WorkSchedules
            .Where(schedule => schedule.EmployeeId == employeeId && schedule.DaysOfWeek.Contains(dayCode))
            .FirstOrDefaultAsync();

        if (employeeSchedule is not null)
        {
            return employeeSchedule;
        }
        
        // fallback department schedule if individual schedule not existed
        var departmentSchedule = await db.WorkSchedules
            .Where(schedule => schedule.DepartmentId == employee.DepartmentId && schedule.DaysOfWeek.Contains(dayCode))
            .FirstOrDefaultAsync();
        return departmentSchedule;
    }

    private string ToDayCode(DayOfWeek dayOfWeek)
    {
        return dayOfWeek switch
        {
            DayOfWeek.Monday => "MON",
            DayOfWeek.Tuesday => "TUE",
            DayOfWeek.Wednesday => "WED",
            DayOfWeek.Thursday => "THU",
            DayOfWeek.Friday => "FRI",
            DayOfWeek.Saturday => "SAT",
            DayOfWeek.Sunday => "SUN",
            _ => throw new ArgumentOutOfRangeException(nameof(dayOfWeek), dayOfWeek, null)
        };
    }
}