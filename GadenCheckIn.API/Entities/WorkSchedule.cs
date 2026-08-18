namespace GadenCheckIn.API.Entities;

public class WorkSchedule
{
    public Guid Id { get; set; }
    
    public Guid? DepartmentId { get; set; }
    public Department Department { get; set; } = null!;
    
    public Guid? EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    
    public required string DaysOfWeek { get; set; }

    public static WorkSchedule Create(
        Guid? departmentId, 
        Guid? employeeId,
        TimeOnly startTime, 
        TimeOnly endTime, 
        string daysOfWeek)
    {
        if ((departmentId != null) == (employeeId != null))
        {
            throw new ArgumentException(
                "WorkSchedule phải gắn với ĐÚNG 1 trong 2: department hoặc employee");
        }
        
        return new WorkSchedule
        {
            DepartmentId = departmentId,
            EmployeeId = employeeId,
            StartTime = startTime,
            EndTime = endTime,
            DaysOfWeek = daysOfWeek,
        };
    }
}