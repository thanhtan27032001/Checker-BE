using System.Text.Json;

namespace GadenCheckIn.API.Entities;

public enum CheckInMethod
{
    Button, Gps, Qr, Face
}

public enum AttendanceStatus
{
    OnTime, Late, EarlyLeave, MissingCheckout, OnLeave
}
public class AttendanceRecord
{
    public Guid Id { get; set; }
    
    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    
    public DateTimeOffset? CheckInTime { get; set; }
    public DateTimeOffset? CheckOutTime { get; set; }
    public CheckInMethod CheckInMethod { get; set; } = CheckInMethod.Button;
    
    public JsonDocument? MetaData { get; set; }
    public required AttendanceStatus Status { get; set; }
}