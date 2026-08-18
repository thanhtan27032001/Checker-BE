using GadenCheckIn.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace GadenCheckIn.API.Data;

public class GadenCheckInDbContext(DbContextOptions<GadenCheckInDbContext> options) : DbContext
{
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<WorkSchedule> WorkSchedules => Set<WorkSchedule>();
    public DbSet<AttendanceRecord> AttendanceRecords => Set<AttendanceRecord>();
    public DbSet<LeaveRequest> LeaveRequests => Set<LeaveRequest>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            entity.SetTableName(ToSnakeCase(entity.GetTableName()!));
            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(ToSnakeCase(property.GetColumnName()));
            }
        }
        
        // composite keys
        modelBuilder.Entity<Employee>()
            .HasIndex(e => new { e.CompanyId, e.Email })
            .IsUnique();
        
        // only allow 1 attendance for 1 day 
        modelBuilder.Entity<AttendanceRecord>()
            .HasIndex(a => a.EmployeeId)
            .HasFilter("check_out_time IS NULL")
            .IsUnique();
        
        // delete company will not let delete other records which FK to that company
        modelBuilder.Entity<AttendanceRecord>()
            .HasOne(a => a.Employee)
            .WithMany(e => e.AttendanceRecords)
            .OnDelete(DeleteBehavior.Restrict);
    }
    
    private static string ToSnakeCase(string input) =>
        string.Concat(input.Select((ch, i) =>
            i > 0 && char.IsUpper(ch) ? "_" + char.ToLower(ch) : char.ToLower(ch).ToString()));
}