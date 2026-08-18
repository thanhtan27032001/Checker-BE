namespace GadenCheckIn.API.Entities;

public enum LeaveType
{
    Annual, Sick, Unpaid
}

public enum LeaveStatus
{
    Pending, Approved, Rejected
}

public class LeaveRequest
{
    public Guid Id { get; set; }
    
    public Guid EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    
    public Guid? ApproverId { get; set; }
    public Employee? Approver { get; set; }

    public LeaveType Type { get; set; } = LeaveType.Annual;
    
    public required DateOnly StartDate { get; set; }
    public required DateOnly EndDate { get; set; }
    
    public string? Reason { get; set; }

    public LeaveStatus Status { get; set; } = LeaveStatus.Pending;
    
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    
    public static LeaveRequest Create(
        Guid employeeId,
        LeaveType type,
        DateOnly startDate,
        DateOnly endDate,
        string? reason)
    {
        if (endDate < startDate)
        {
            throw new ArgumentException("endDate must >= startDate");
        }

        return new LeaveRequest
        {
            EmployeeId = employeeId,
            Type = type,
            StartDate = startDate,
            EndDate = endDate,
            Reason = reason
        };
    }
}