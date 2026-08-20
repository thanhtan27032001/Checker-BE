using GadenCheckIn.API.Dtos.Employee;

namespace GadenCheckIn.API.Services;

public interface IEmployeeService
{
    Task<EmployeeResponseDto> CreateAsync(EmployeeCreateDto dto);
    Task<EmployeeResponseDto> GetByIdAsync(Guid id);
    Task<List<EmployeeResponseDto>> GetAllByCompanyAsync(Guid companyId);
}