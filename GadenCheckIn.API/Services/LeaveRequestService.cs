using GadenCheckIn.API.Common.Exceptions;
using GadenCheckIn.API.Data;
using GadenCheckIn.API.Dtos.LeaveRequest;
using GadenCheckIn.API.Entities;
using GadenCheckIn.API.Mappers;
using Microsoft.EntityFrameworkCore;

namespace GadenCheckIn.API.Services;

public class LeaveRequestService(GadenCheckInDbContext db) : ILeaveRequestService
{
    public async Task<LeaveRequestResponseDto> CreateAsync(LeaveRequestCreateDto dto)
    {
        var employeeExists = await db.Employees.AnyAsync(e => e.Id == dto.EmployeeId);
        if (!employeeExists)
        {
            throw new NotFoundException($"No employee found with id {dto.EmployeeId}");
        }
        var entity = LeaveRequest.Create(
            dto.EmployeeId,
            dto.Type,
            dto.StartDate,
            dto.EndDate,
            dto.Reason);
        db.LeaveRequests.Add(entity);
        await db.SaveChangesAsync();

        await db.Entry(entity).Reference(leaveRequest => leaveRequest.Employee).LoadAsync();
        return entity.ToLeaveRequestResponseDto();
    }

    public async Task<LeaveRequestResponseDto> ApproveAsync(Guid id, LeaveRequestReviewDto dto)
    {
        // check leave request existed and match condition
        var entity = await GetPendingOrThrow(id);

        // approve request
        entity.Status = LeaveStatus.Approved;
        entity.ApproverId = dto.ApproverId;
        
        // save and reload the approver
        await db.SaveChangesAsync();
        await db.Entry(entity).Reference(leaveRequest => leaveRequest.Approver).LoadAsync();
        
        return entity.ToLeaveRequestResponseDto();
    }

    public async Task<LeaveRequestResponseDto> RejectAsync(Guid id, LeaveRequestReviewDto dto)
    {
        // check existed and match condition leave request
        var entity = await GetPendingOrThrow(id);
        
        // reject leave request
        entity.Status = LeaveStatus.Rejected;
        entity.ApproverId = dto.ApproverId;
        
        // save and reload
        await db.SaveChangesAsync();
        await db.Entry(entity).Reference(leaveRequest => leaveRequest.Approver).LoadAsync();

        return entity.ToLeaveRequestResponseDto();
    }

    public async Task<List<LeaveRequestResponseDto>> GetByEmployeeAsync(Guid employeeId)
    {
        var leaveRequests = await db.LeaveRequests
            .Include(request => request.Employee)
            .Include(request => request.Approver)
            .Where(request => request.EmployeeId == employeeId)
            .OrderByDescending(leaveRequest => leaveRequest.CreatedAt)
            .ToListAsync();
        return leaveRequests.Select(request => request.ToLeaveRequestResponseDto()).ToList();
    }
    
    private async Task<LeaveRequest> GetPendingOrThrow(Guid id)
    {
        var entity = await db.LeaveRequests
            .Include(request => request.Employee)
            .FirstOrDefaultAsync(request => request.Id == id);
        
        if (entity == null)
        {
            throw new NotFoundException($"No leave request found with id {id}");
        }

        if (entity.Status != LeaveStatus.Pending)
        {
            throw new BusinessRuleException($"The leave request status {entity.Status} is not pending.");
        }

        return entity;
    }
}