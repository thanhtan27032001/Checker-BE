namespace GadenCheckIn.API.Dtos.WorkSchedule;

public record WorkScheduleResponseDto
{
    public required Guid Id { get; init; }
    public Guid? DepartmentId { get; init; }
    public string? DepartmentName { get; init; }
    public Guid? EmployeeId { get; init; }
    public string? EmployeeName { get; init; }
    public required TimeOnly StartTime { get; init; }
    public required TimeOnly EndTime { get; init; }
    public required List<string> DaysOfWeek { get; init; }
}