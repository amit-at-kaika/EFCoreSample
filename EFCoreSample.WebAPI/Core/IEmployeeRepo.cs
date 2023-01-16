using EFCoreSample.WebAPI.Models;

namespace EFCoreSample.WebAPI.Core;

public interface IEmployeeRepo : IGenericRepo<Employee>
{
    // To add functionality that is specific to Employee table, to be overridden in the impl class
}
