using GadenCheckIn.API.Entities;

namespace GadenCheckIn.API.Dtos.Attendence;

public class AttendanceResponseDto
{
    public required Guid Id { get; init; }
    public required Guid EmployeeId { get; init; }
    public required string EmployeeName { get; init; }
    public DateTimeOffset? CheckInTime { get; init; }
    public DateTimeOffset? CheckOutTime { get; init; }
    public required CheckInMethod CheckInMethod { get; init; }
    public required AttendanceStatus Status { get; init; }
    public double? WorkedHours { get; init; }
}