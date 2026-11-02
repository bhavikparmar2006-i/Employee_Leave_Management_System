using EmployeeLeaveManagementSystem.Data;
using EmployeeLeaveManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace EmployeeLeaveManagementSystem.Controllers;
[Authorize]
public class DashboardController : Controller
{
    private readonly ApplicationDbContext _db; private readonly UserManager<ApplicationUser> _users;
    public DashboardController(ApplicationDbContext db, UserManager<ApplicationUser> users) { _db=db; _users=users; }
    public async Task<IActionResult> Index()
    {
        if (User.IsInRole("Admin")) {
            ViewBag.TotalEmployees = await _db.Employees.CountAsync();
            ViewBag.Pending = await _db.LeaveRequests.CountAsync(x=>x.Status==LeaveStatus.Pending);
            ViewBag.Approved = await _db.LeaveRequests.CountAsync(x=>x.Status==LeaveStatus.Approved);
            ViewBag.Rejected = await _db.LeaveRequests.CountAsync(x=>x.Status==LeaveStatus.Rejected);
            ViewBag.Recent = await _db.LeaveRequests.Include(x=>x.Employee).OrderByDescending(x=>x.AppliedOn).Take(5).ToListAsync();
            return View("Admin");
        }
        var currentUser = await _users.GetUserAsync(User);
        var email=currentUser?.Email;
        var employee=await _db.Employees.FirstOrDefaultAsync(e=>e.UserId==_users.GetUserId(User) || e.Email==email);
        if (employee is null) return View("ProfileMissing");
        var leaves=await _db.LeaveRequests.Where(l=>l.EmployeeId==employee.Id).OrderByDescending(l=>l.AppliedOn).Take(8).ToListAsync();
        ViewBag.Employee=employee; return View("Employee", leaves);
    }
}
