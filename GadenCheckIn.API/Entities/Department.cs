namespace GadenCheckIn.API.Entities;

public class Department
{
    public Guid Id { get; set; }
    public Guid companyId { get; set; }
    public Company company { get; set; } = null!;
    public required string Name { get; set; }
    public Guid? ManagerId { get; set; }
    public Employee? Manager { get; set; }
}