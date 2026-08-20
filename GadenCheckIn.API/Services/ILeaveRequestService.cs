using GadenCheckIn.API.Dtos.LeaveRequest;

namespace GadenCheckIn.API.Services;

public interface ILeaveRequestService
{
    Task<LeaveRequestResponseDto> CreateAsync(LeaveRequestCreateDto dto);
    Task<LeaveRequestResponseDto> ApproveAsync(Guid id, LeaveRequestReviewDto dto);
    Task<LeaveRequestResponseDto> RejectAsync(Guid id, LeaveRequestReviewDto dto);
    Task<List<LeaveRequestResponseDto>> GetByEmployeeAsync(Guid employeeId);
}