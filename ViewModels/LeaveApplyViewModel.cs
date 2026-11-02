using EmployeeLeaveManagementSystem.Models;
using System.ComponentModel.DataAnnotations;
namespace EmployeeLeaveManagementSystem.ViewModels;
public class LeaveApplyViewModel
{
    [Required] public LeaveType LeaveType { get; set; }
    [Required, DataType(DataType.Date)] public DateTime StartDate { get; set; } = DateTime.Today;
    [Required, DataType(DataType.Date)] public DateTime EndDate { get; set; } = DateTime.Today;
    [Required, StringLength(500, MinimumLength = 5)] public string Reason { get; set; } = string.Empty;
}
public class LeaveDecisionViewModel
{
    public int Id { get; set; }
    [StringLength(500)] public string? AdminComment { get; set; }
}
