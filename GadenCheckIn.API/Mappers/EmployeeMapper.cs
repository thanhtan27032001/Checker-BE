using GadenCheckIn.API.Dtos.Employee;
using GadenCheckIn.API.Entities;

namespace GadenCheckIn.API.Mappers;

public static class EmployeeMapper
{
    public static EmployeeResponseDto ToResponseDto(
        this Employee entity)
    {
        return new EmployeeResponseDto
        {
            Id = entity.Id,
            FullName = entity.FullName,
            Email = entity.Email,
            CompanyId = entity.CompanyId,
            Role = entity.Role,
            Status = entity.Status,
            CreatedAt = entity.CreatedAt,
        };
    }
}