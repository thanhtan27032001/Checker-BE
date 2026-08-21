using GadenCheckIn.API.Dtos.WorkSchedule;
using GadenCheckIn.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace GadenCheckIn.API.Controllers;

[ApiController]
[Route("api/work-schedules")]
public class WorkScheduleController(IWorkScheduleService workScheduleService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] WorkScheduleCreateDto dto)
    {
        var result = await workScheduleService.CreateAsync(dto);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await workScheduleService.GetAllAsync();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await workScheduleService.DeleteAsync(id);
        return NoContent();
    }
}