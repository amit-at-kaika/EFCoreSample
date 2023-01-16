namespace EFCoreSample.WebAPI.Core;

public interface IUnitOfWork 
{
    public IEmployeeRepo Employees { get; }
    Task CompleteAsync();
}