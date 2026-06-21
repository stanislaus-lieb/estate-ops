using EstateOps.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EstateOps.Infrastructure.Persistence;

public sealed class EstateOpsDbContextFactory : IDesignTimeDbContextFactory<EstateOpsDbContext>
{
  public EstateOpsDbContext CreateDbContext(string[] args)
  {
    var options = new DatabaseOptions();
    var connectionString = DatabaseConnectionStringFactory.Create(options);
    var builder = new DbContextOptionsBuilder<EstateOpsDbContext>();

    builder.UseNpgsql(connectionString, npgsql => npgsql.MigrationsAssembly(typeof(EstateOpsDbContext).Assembly.FullName));

    return new EstateOpsDbContext(builder.Options);
  }
}
