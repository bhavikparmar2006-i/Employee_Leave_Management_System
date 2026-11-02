using EmployeeLeaveManagementSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveManagementSystem.Data;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<LeaveRequest> LeaveRequests => Set<LeaveRequest>();
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Employee>().HasIndex(e => e.Email).IsUnique();
        builder.Entity<Employee>().HasOne(e => e.User).WithOne().HasForeignKey<Employee>(e => e.UserId).OnDelete(DeleteBehavior.SetNull);
        builder.Entity<LeaveRequest>().HasOne(l => l.Employee).WithMany(e => e.LeaveRequests).HasForeignKey(l => l.EmployeeId).OnDelete(DeleteBehavior.Restrict);
        builder.Entity<LeaveRequest>().Property(l => l.Status).HasConversion<string>();
        builder.Entity<LeaveRequest>().Property(l => l.LeaveType).HasConversion<string>();
    }
}
