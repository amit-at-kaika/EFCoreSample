using EFCoreSample.WebAPI.Core;

namespace EFCoreSample.WebAPI.Data;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    readonly ApiDbContext _dbContext;
    readonly ILogger _logger;

    public IEmployeeRepo Employees { get; private set;}

    public UnitOfWork(ApiDbContext dbContext, ILoggerFactory loggerFactory)
    {
        _dbContext = dbContext;
        _logger = loggerFactory.CreateLogger("Logs");

        Employees = new EmployeeRepo(dbContext, _logger);
    }

    public async Task CompleteAsync()
    {
         await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }

}