using GadenCheckIn.API.Dtos.Employee;
using GadenCheckIn.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace GadenCheckIn.API.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeesController(IEmployeeService employeeService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] EmployeeCreateDto dto)
    {
        var result = await employeeService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await employeeService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid companyId)
    {
        var result = await employeeService.GetAllByCompanyAsync(companyId);
        return Ok(result);
    }
}