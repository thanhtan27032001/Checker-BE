using System.ComponentModel.DataAnnotations;

public record EmployeeCreateDto
{
    [Required]
    public Guid CompanyId { get; init; }
    
    public Guid? DepartmentId { get; init; }
    
    [Required, MaxLength(50)]
    public required string FullName { get; init; }
    
    [Required, EmailAddress]
    public required string Email { get; init; }
    
    [Required, MinLength(8)]
    public required string Password { get; init; }
}