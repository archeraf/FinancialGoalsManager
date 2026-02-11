using FinancialGoalsManager.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class SqlServerContextFactory : IDesignTimeDbContextFactory<SqlServerContext>
{
    public SqlServerContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SqlServerContext>();
        optionsBuilder.UseSqlServer("Server=localhost;Database=FinancialGoalsManager;User Id=sa;Password=MY_SENHA123!;TrustServerCertificate=True");

        return new SqlServerContext(optionsBuilder.Options);
    }
}