using EFCoreSample.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreSample.WebAPI.Data;

public class ApiDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }

    public ApiDbContext(DbContextOptions<ApiDbContext> options)
        : base(options)
    {

    }
}