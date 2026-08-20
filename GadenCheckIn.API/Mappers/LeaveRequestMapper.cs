using GadenCheckIn.API.Dtos.LeaveRequest;
using GadenCheckIn.API.Entities;

namespace GadenCheckIn.API.Mappers;

public static class LeaveRequestMapper
{
    public static LeaveRequestResponseDto ToLeaveRequestResponseDto(this LeaveRequest entity)
    {
        return new LeaveRequestResponseDto
        {
            Id = entity.Id,
            EmployeeId = entity.EmployeeId,
            EmployeeName = entity.Employee?.FullName ?? string.Empty,
            ApproverId = entity.ApproverId,
            ApproverName = entity.Approver?.FullName,
            Type = entity.Type,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Reason = entity.Reason,
            Status = entity.Status,
            CreatedAt = entity.CreatedAt,
            TotalDays = entity.EndDate.DayNumber - entity.StartDate.DayNumber + 1,
        };
    }
}