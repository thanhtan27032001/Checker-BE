using GadenCheckIn.API.Entities;

namespace GadenCheckIn.API.Dtos.LeaveRequest;

public record LeaveRequestResponseDto
{
    public required Guid Id { get; init; }
    public required Guid EmployeeId { get; init; }
    public required string EmployeeName { get; init; }
    public Guid? ApproverId { get; init; }
    public string? ApproverName { get; init; }
    public required LeaveType Type { get; init; }
    public required DateOnly StartDate { get; init; }
    public required DateOnly EndDate { get; init; }
    public string? Reason { get; init; }
    public required LeaveStatus Status { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }

    // Computed field
    public required int TotalDays { get; init; }
}