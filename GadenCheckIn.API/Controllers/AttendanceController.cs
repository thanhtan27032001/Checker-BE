using GadenCheckIn.API.Dtos.Attendence;
using GadenCheckIn.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace GadenCheckIn.API.Controllers;

[ApiController]
[Route("api/attendance")]
public class AttendanceController(IAttendanceService service) : ControllerBase
{
    [HttpPost("check-in")]
    public async Task<IActionResult> CheckIn([FromBody] CheckInDto dto)
    {
        var result = await service.CheckIn(dto);
        return Ok(result);
    }

    [HttpPost("check-out")]
    public async Task<IActionResult> CheckOut([FromQuery] Guid employeeId)
    {
        var result = await service.CheckOut(employeeId);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await service.GetAll();
        return Ok(result);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await service.Delete(id);
        return NoContent();
    }
}