using GadenCheckIn.API.Entities;

namespace GadenCheckIn.API.Services;

public interface IWorkScheduleService
{
    Task<WorkSchedule?> GetApplicableScheduleAsync(Guid employeeId, DayOfWeek dayOfWeek);
}