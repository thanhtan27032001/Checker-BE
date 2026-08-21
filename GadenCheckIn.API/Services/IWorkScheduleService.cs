using GadenCheckIn.API.Dtos.WorkSchedule;
using GadenCheckIn.API.Entities;

namespace GadenCheckIn.API.Services;

public interface IWorkScheduleService
{
    Task<WorkSchedule?> GetApplicableScheduleAsync(Guid employeeId, DayOfWeek dayOfWeek);
    Task<WorkScheduleResponseDto> CreateAsync(WorkScheduleCreateDto dto);
    Task<List<WorkScheduleResponseDto>> GetAllAsync();
    Task DeleteAsync(Guid id);
}