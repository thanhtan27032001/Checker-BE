using System.ComponentModel.DataAnnotations;

namespace GadenCheckIn.API.Dtos.LeaveRequest;

public record LeaveRequestReviewDto
{
    [Required]
    public required Guid ApproverId { get; init; }
}