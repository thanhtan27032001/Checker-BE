using GadenCheckIn.API.Dtos.LeaveRequest;
using GadenCheckIn.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace GadenCheckIn.API.Controllers;

[ApiController]
[Route("api/leave-requests")]
public class LeaveRequestController(ILeaveRequestService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateLeaveRequest([FromBody] LeaveRequestCreateDto dto)
    {
        var result = await service.CreateAsync(dto);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/approve")]
    public async Task<IActionResult> ApproveLeaveRequest(Guid id, [FromBody] LeaveRequestReviewDto dto)
    {
        var result = await service.ApproveAsync(id, dto);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/reject")]
    public async Task<IActionResult> RejectLeaveRequest(Guid id, [FromBody] LeaveRequestReviewDto dto)
    {
        var result = await service.RejectAsync(id, dto);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetLeaveRequestsByEmployee([FromQuery] Guid employeeId)
    {
        var result = await service.GetByEmployeeAsync(employeeId);
        return Ok(result);
    }
}