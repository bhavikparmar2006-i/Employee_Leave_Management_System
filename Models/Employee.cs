using System.ComponentModel.DataAnnotations;
namespace EmployeeLeaveManagementSystem.Models;
public class Employee
{
    public int Id { get; set; }
    [Required, StringLength(100)] public string FullName { get; set; } = string.Empty;
    [Required, EmailAddress, StringLength(256)] public string Email { get; set; } = string.Empty;
    [Required, StringLength(80)] public string Department { get; set; } = string.Empty;
    [Required, StringLength(80)] public string Designation { get; set; } = string.Empty;
    [DataType(DataType.Date)] public DateTime JoiningDate { get; set; } = DateTime.Today;
    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }
    public List<LeaveRequest> LeaveRequests { get; set; } = new();
}
