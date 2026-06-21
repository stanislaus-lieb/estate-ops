using EstateOps.Infrastructure.Configuration;
using EstateOps.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EstateOps.Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
  public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    ArgumentNullException.ThrowIfNull(services);
    ArgumentNullException.ThrowIfNull(configuration);

    services.Configure<DatabaseOptions>(options =>
    {
      var section = configuration.GetSection(DatabaseOptions.SectionName);

      options.Host = section[nameof(DatabaseOptions.Host)] ?? options.Host;
      options.Database = section[nameof(DatabaseOptions.Database)] ?? options.Database;
      options.MaintenanceDatabase = section[nameof(DatabaseOptions.MaintenanceDatabase)] ?? options.MaintenanceDatabase;
      options.Username = section[nameof(DatabaseOptions.Username)] ?? options.Username;
      options.Password = section[nameof(DatabaseOptions.Password)] ?? options.Password;
      options.SslMode = section[nameof(DatabaseOptions.SslMode)] ?? options.SslMode;
      options.GssEncryptionMode = section[nameof(DatabaseOptions.GssEncryptionMode)] ?? options.GssEncryptionMode;

      if (int.TryParse(section[nameof(DatabaseOptions.Port)], out var port))
      {
        options.Port = port;
      }
    });

    services.AddDbContext<EstateOpsDbContext>((serviceProvider, options) =>
    {
      var databaseOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
      var connectionString = DatabaseConnectionStringFactory.Create(databaseOptions);
      options.UseNpgsql(connectionString, npgsql => npgsql.MigrationsAssembly(typeof(EstateOpsDbContext).Assembly.FullName));
    });

    services.AddTransient<DatabaseProvisioner>();

    return services;
  }
}
