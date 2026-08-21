using System.ComponentModel.DataAnnotations;

namespace GadenCheckIn.API.Dtos.WorkSchedule;

public record WorkScheduleCreateDto
{
    public Guid? DepartmentId { get; init; }
    public Guid? EmployeeId { get; init; }

    [Required]
    public required TimeOnly StartTime { get; init; }

    [Required]
    public required TimeOnly EndTime { get; init; }

    [Required, MinLength(1)]
    public required List<string> DaysOfWeek { get; init; }
}