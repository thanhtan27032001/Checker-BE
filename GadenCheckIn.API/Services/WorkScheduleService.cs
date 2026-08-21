using GadenCheckIn.API.Common.Exceptions;
using GadenCheckIn.API.Data;
using GadenCheckIn.API.Dtos.WorkSchedule;
using GadenCheckIn.API.Entities;
using GadenCheckIn.API.Mappers;
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

    public async Task<WorkScheduleResponseDto> CreateAsync(WorkScheduleCreateDto dto)
    {
        var entity = WorkSchedule.Create(
            dto.DepartmentId, dto.EmployeeId, dto.StartTime, dto.EndTime, string.Join(',', dto.DaysOfWeek));
        db.WorkSchedules.Add(entity);
        await db.SaveChangesAsync();
        
        if (entity.DepartmentId is not null)
        {
            await db.Entry(entity).Reference(ws => ws.Department).LoadAsync();
        }
        if (entity.EmployeeId is not null)
        {
            await db.Entry(entity).Reference(ws => ws.Employee).LoadAsync();
        }

        return entity.ToWorkScheduleResponseDto();
    }

    public async Task<List<WorkScheduleResponseDto>> GetAllAsync()
    {
        var records = await db.WorkSchedules
            .Include(workSchedule => workSchedule.Department)
            .Include(workSchedule => workSchedule.Employee)
            .ToListAsync();
        return records.Select(record => record.ToWorkScheduleResponseDto()).ToList();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await db.WorkSchedules.FindAsync(id);
        if (entity is null)
        {
            throw new NotFoundException($"No work schedule found with id {id}");
        }

        db.WorkSchedules.Remove(entity);
        await db.SaveChangesAsync();
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