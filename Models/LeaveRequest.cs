using System.ComponentModel.DataAnnotations;
namespace EmployeeLeaveManagementSystem.Models;
public enum LeaveType { Casual, Sick, Earned }
public enum LeaveStatus { Pending, Approved, Rejected, Cancelled }
public class LeaveRequest
{
    public int Id { get; set; }
    [Required] public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    [Required] public LeaveType LeaveType { get; set; }
    [Required, DataType(DataType.Date)] public DateTime StartDate { get; set; }
    [Required, DataType(DataType.Date)] public DateTime EndDate { get; set; }
    [Required, StringLength(500, MinimumLength = 5)] public string Reason { get; set; } = string.Empty;
    public LeaveStatus Status { get; set; } = LeaveStatus.Pending;
    public DateTime AppliedOn { get; set; } = DateTime.UtcNow;
    [StringLength(500)] public string? AdminComment { get; set; }
}
