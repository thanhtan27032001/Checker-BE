using System.ComponentModel.DataAnnotations;
using GadenCheckIn.API.Entities;

namespace GadenCheckIn.API.Dtos.Attendence;

public record CheckInDto
{
    [Required]
    public required Guid EmployeeId { get; init; }
    public required CheckInMethod Method { get; init; } = CheckInMethod.Button;
    public Dictionary<string, object>? Metadata { get; init; }
}