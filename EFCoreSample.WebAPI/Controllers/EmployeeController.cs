using EFCoreSample.WebAPI.Data;
using EFCoreSample.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreSample.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    ILogger<EmployeeController> _logger;
    readonly ApiDbContext _dbContext;
    
    public EmployeeController(ILogger<EmployeeController> logger, ApiDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Employee>))]
    public async Task<IActionResult> GetAsync()
    {
        return Ok(await _dbContext.Employees.ToListAsync());
    }

    [HttpGet("{employeeId}")]
    [ProducesResponseType(200, Type = typeof(Employee))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(int employeeId)
    {
        Employee? employee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);

        if (employee is null)
            return NotFound();
        return Ok(employee);
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(Employee))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
    {
        if (employee is null)
            return BadRequest();

        _dbContext.Employees.Add(employee);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("employeeId")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteEmployee(int employeeId)
    {
        Employee? employee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
        if (employee is null)
            return NotFound();

        _dbContext.Employees.Remove(employee);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpPatch]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateEmployee(Employee employee)
    {
        Employee? existingEmployee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);
         if (existingEmployee is null)
            return NotFound();
        
        existingEmployee.Name = employee.Name;
        existingEmployee.Department = employee.Department;

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }
}