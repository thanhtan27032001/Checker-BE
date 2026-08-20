using GadenCheckIn.API.Entities;

namespace GadenCheckIn.API.Dtos.Employee;

public record EmployeeResponseDto
{
    public required Guid Id { get; init; }
    public required Guid CompanyId { get; init; }
    public Guid? DepartmentId { get; init; }
    public required string FullName { get; init; }
    public required string Email { get; init; }
    public required EmployeeRole Role { get; init; }
    public required EmployeeStatus Status { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
}