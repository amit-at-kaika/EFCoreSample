using EFCoreSample.WebAPI.Core;
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
    readonly IUnitOfWork _unitOfWork;
    
    public EmployeeController(ILogger<EmployeeController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Employee>))]
    public async Task<IActionResult> GetAsync()
    {
        return Ok(await _unitOfWork.Employees.GetAll());
    }

    [HttpGet("{employeeId}")]
    [ProducesResponseType(200, Type = typeof(Employee))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(int employeeId)
    {
        Employee? employee = await _unitOfWork.Employees.GetById(employeeId);

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

        await _unitOfWork.Employees.Add(employee);
        await _unitOfWork.CompleteAsync();

        return Ok();
    }

    [HttpDelete("employeeId")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteEmployee(int employeeId)
    {
        Employee? employee = await _unitOfWork.Employees.GetById(employeeId);
        if (employee is null)
            return NotFound();

        await _unitOfWork.Employees.Delete(employee);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }

    [HttpPatch]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateEmployee(Employee employee)
    {
        Employee? existingEmployee = await _unitOfWork.Employees.GetById(employee.EmployeeId);
         if (existingEmployee is null)
            return NotFound();
        
        existingEmployee.Name = employee.Name;
        existingEmployee.Department = employee.Department;

        await _unitOfWork.Employees.Update(existingEmployee);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}