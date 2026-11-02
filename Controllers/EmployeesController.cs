using EmployeeLeaveManagementSystem.Data;
using EmployeeLeaveManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
namespace EmployeeLeaveManagementSystem.Controllers;

[Authorize(Roles = "Admin")]
public class EmployeesController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public EmployeesController(
        ApplicationDbContext db,
        UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }
    public async Task<IActionResult> Index(string? search) { var q = _db.Employees.AsQueryable(); if (!string.IsNullOrWhiteSpace(search)) q = q.Where(e => e.FullName.Contains(search) || e.Department.Contains(search) || e.Email.Contains(search)); ViewBag.Search = search; return View(await q.OrderBy(e => e.FullName).ToListAsync()); }
    public IActionResult Create() => View(new Employee());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Employee employee)
    {
        if (await _db.Employees.AnyAsync(e => e.Email == employee.Email))
        {
            ModelState.AddModelError(
                nameof(employee.Email),
                "An employee with this email already exists.");
        }

        if (!ModelState.IsValid)
            return View(employee);

        var email = employee.Email.Trim();

        // Do not create a duplicate login account.
        var existingUser = await _userManager.FindByEmailAsync(email);

        if (existingUser != null)
        {
            ModelState.AddModelError(
                nameof(employee.Email),
                "A login account with this email already exists. Use a different email.");
            return View(employee);
        }

        // Generate a temporary password for the new employee.
        var temporaryPassword =
            Convert.ToBase64String(RandomNumberGenerator.GetBytes(12)) + "aA1!";

        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true,
            FullName = employee.FullName
        };

        var result = await _userManager.CreateAsync(user, temporaryPassword);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(employee);
        }

        var roleResult = await _userManager.AddToRoleAsync(user, "Employee");

        if (!roleResult.Succeeded)
        {
            await _userManager.DeleteAsync(user);

            foreach (var error in roleResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(employee);
        }

        employee.Email = email;
        employee.UserId = user.Id;
        employee.User = null;

        try
        {
            _db.Employees.Add(employee);
            await _db.SaveChangesAsync();
        }
        catch
        {
            // Avoid leaving a login account without an employee profile.
            await _userManager.DeleteAsync(user);

            ModelState.AddModelError(
                string.Empty,
                "Could not save the employee profile. Please check the database and try again.");

            return View(employee);
        }

        TempData["Success"] = "Employee created successfully.";
        TempData["NewEmployeeEmail"] = email;
        TempData["NewEmployeePassword"] = temporaryPassword;

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id) { var e = await _db.Employees.FindAsync(id); return e is null ? NotFound() : View(e); }
    [HttpPost, ValidateAntiForgeryToken] public async Task<IActionResult> Edit(int id, Employee employee) { if (id != employee.Id) return BadRequest(); if (await _db.Employees.AnyAsync(e => e.Email == employee.Email && e.Id != id)) ModelState.AddModelError(nameof(employee.Email), "Email is already used."); if (!ModelState.IsValid) return View(employee); _db.Entry(employee).State = EntityState.Modified; _db.Entry(employee).Property(x => x.UserId).IsModified = false; await _db.SaveChangesAsync(); TempData["Success"] = "Employee updated."; return RedirectToAction(nameof(Index)); }
    public async Task<IActionResult> Details(int id) { var e = await _db.Employees.Include(x => x.LeaveRequests).FirstOrDefaultAsync(x => x.Id == id); return e is null ? NotFound() : View(e); }
    public async Task<IActionResult> Delete(int id) { var e = await _db.Employees.FindAsync(id); return e is null ? NotFound() : View(e); }
    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken] public async Task<IActionResult> DeleteConfirmed(int id) { var e = await _db.Employees.Include(x => x.LeaveRequests).FirstOrDefaultAsync(x => x.Id == id); if (e is null) return NotFound(); if (e.LeaveRequests.Count > 0) { TempData["Error"] = "This employee has leave history and cannot be deleted."; return RedirectToAction(nameof(Index)); } _db.Employees.Remove(e); await _db.SaveChangesAsync(); TempData["Success"] = "Employee deleted."; return RedirectToAction(nameof(Index)); }
}
