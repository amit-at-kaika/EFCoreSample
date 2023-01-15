using EFCoreSample.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreSample.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    ILogger<EmployeeController> _logger;
    static List<Employee> _employees = new List<Employee>
    {
        new Employee()
        {
            Name = "Iya",
            EmployeeId = 1,
            Department = "Human Resources"
        },
        new Employee()
        {
            Name = "Dada",
            EmployeeId = 2,
            Department = "Accounts"
        },
        new Employee()
        {
            Name = "Aai",
            EmployeeId = 3,
            Department = "CEO"
        }

    };

    public EmployeeController(ILogger<EmployeeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Employee>))]
    public IActionResult Get()
    {
        return Ok(_employees);
    }

    [HttpGet("{employeeId}")]
    [ProducesResponseType(200, Type = typeof(Employee))]
    [ProducesResponseType(404)]
    public IActionResult GetById(int employeeId)
    {
        Employee employee = _employees.FirstOrDefault(e => e.EmployeeId == employeeId)!;

        if (employee is null)
            return NotFound();
        return Ok(employee);
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(Employee))]
    [ProducesResponseType(400)]
    public IActionResult AddEmployee([FromBody] Employee employee)
    {
        if (employee is null)
            return BadRequest();
        _employees.Add(employee);
        return Ok();
    }

    [HttpDelete("employeeId")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult DeleteEmployee(int employeeId)
    {
        Employee? employee = _employees.FirstOrDefault(e => e.EmployeeId == employeeId);
        if (employee is null)
            return NotFound();

        _employees.Remove(employee);

        return NoContent();
    }

    [HttpPatch]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult UpdateEmployee(Employee employee)
    {
        Employee? exitingEmployee = _employees.FirstOrDefault(e => e.EmployeeId == employee.EmployeeId);
         if (exitingEmployee is null)
            return NotFound();
        
        exitingEmployee.Name = employee.Name;
        exitingEmployee.Department = employee.Department;

        return NoContent();
    }
}