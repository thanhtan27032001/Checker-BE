namespace GadenCheckIn.API.Entities;

public enum SubscriptionTier
{
    Free, Premium
}

public class Company
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string CompanyCode { get; set; }
    public required SubscriptionTier SubscriptionTier { get; set; } = SubscriptionTier.Free;
    public required DateTime CreatedAt { get; set; }

    public ICollection<Department> Departments { get; set; } = [];
    public ICollection<Employee> Employees { get; set; } = [];
}