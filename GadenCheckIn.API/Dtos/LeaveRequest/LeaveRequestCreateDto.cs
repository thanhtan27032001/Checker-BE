using System.ComponentModel.DataAnnotations;
using GadenCheckIn.API.Entities;

namespace GadenCheckIn.API.Dtos.LeaveRequest;

public record LeaveRequestCreateDto
{
    [Required]
    public required Guid EmployeeId { get; init; }

    public LeaveType Type { get; init; } = LeaveType.Annual;

    [Required]
    public required DateOnly StartDate { get; init; }

    [Required]
    public required DateOnly EndDate { get; init; }

    [MaxLength(500)]
    public string? Reason { get; init; }
}