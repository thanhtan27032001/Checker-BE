using GadenCheckIn.API.Common.Exceptions;
using GadenCheckIn.API.Data;
using GadenCheckIn.API.Dtos.Employee;
using GadenCheckIn.API.Entities;
using GadenCheckIn.API.Mappers;
using Microsoft.EntityFrameworkCore;

namespace GadenCheckIn.API.Services;

public class EmployeeService(GadenCheckInDbContext db) : IEmployeeService
{
    public async Task<EmployeeResponseDto> CreateAsync(EmployeeCreateDto dto)
    {
        var emailExists = await db.Employees.AnyAsync(
            e => e.CompanyId == dto.CompanyId && e.Email == dto.Email);
        if (emailExists)
        {
            throw new BusinessRuleException($"Email {dto.Email} is already taken");
        }
        
        var entity = new Employee
        {
            Id = Guid.NewGuid(),
            CompanyId = dto.CompanyId,
            DepartmentId = dto.DepartmentId,
            FullName = dto.FullName,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
        };
        db.Employees.Add(entity);
        await db.SaveChangesAsync();
        
        return entity.ToResponseDto();
    }

    public async Task<EmployeeResponseDto> GetByIdAsync(Guid id)
    {
        var entity = await db.Employees.FindAsync(id);

        if (entity is null)
        {
            throw new NotFoundException($"No employee with id {id} was found");
        }

        return entity.ToResponseDto();
    }

    public async Task<List<EmployeeResponseDto>> GetAllByCompanyAsync(Guid companyId)
    {
        return await db.Employees
            .Where(e => e.CompanyId == companyId)
            .Select(e => e.ToResponseDto())
            .ToListAsync();
    }
}