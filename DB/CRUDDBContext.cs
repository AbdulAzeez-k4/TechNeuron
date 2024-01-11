using CRUD.DB;
using Microsoft.EntityFrameworkCore;

public class CRUDDBContext : DbContext
{
    private readonly string _connectionString;

    public CRUDDBContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }

    public DbSet<EmployeeDomain> Employee { get; set; }
}
