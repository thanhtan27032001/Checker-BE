namespace GadenCheckIn.API.Entities;

public enum EmployeeRole { Admin, Manager, Staff }
public enum EmployeeStatus { Active, Inactive }

public class Employee
{
    public Guid Id { get; set; }
    
    public Guid CompanyId { get; set; }
    public Company Company { get; set; } = null!;
    
    public Guid? DepartmentId { get; set; }
    public Department? Department { get; set; }
    
    public required string FullName { get; set; }
    public required string Email { get; set; }
    
    public required string PasswordHash { get; set; }
    
    public EmployeeRole Role { get; set; } = EmployeeRole.Staff;
    public EmployeeStatus Status { get; set; } = EmployeeStatus.Active;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
 
    public ICollection<AttendanceRecord> AttendanceRecords { get; set; } = [];
    public ICollection<LeaveRequest> LeaveRequests { get; set; } = [];
}