using EFCoreSample.WebAPI.Data;
using EFCoreSample.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreSample.WebAPI.Core;

public class EmployeeRepo : GenericRepo<Employee>, IEmployeeRepo
{
    public EmployeeRepo(ApiDbContext dbContext, ILogger logger) : base(dbContext, logger)
    {
    }

    public override async Task<IEnumerable<Employee>> GetAll()
    {
        try
        {
            return await _dbContext.Employees.Where( e => e.EmployeeId < 10).ToListAsync();
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<Employee>> GetEmployeesByDept(string deptName) 
    {
        try
        {
            return await _dbContext.Employees.Where( e => e.Department.Contains(deptName)).ToListAsync();
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
}