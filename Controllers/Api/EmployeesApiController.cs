using EmployeeLeaveManagementSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace EmployeeLeaveManagementSystem.Controllers.Api;
[ApiController,Route("api/employees"),Authorize(Roles="Admin")]
public class EmployeesApiController(ApplicationDbContext db) : ControllerBase
{
    [HttpGet] public async Task<IActionResult> GetAll()=>Ok(await db.Employees.AsNoTracking().Select(e=>new {e.Id,e.FullName,e.Email,e.Department,e.Designation,e.JoiningDate}).ToListAsync());
    [HttpGet("{id:int}")] public async Task<IActionResult> Get(int id){var e=await db.Employees.AsNoTracking().Where(e=>e.Id==id).Select(e=>new {e.Id,e.FullName,e.Email,e.Department,e.Designation,e.JoiningDate}).FirstOrDefaultAsync();return e is null?NotFound():Ok(e);}
}
