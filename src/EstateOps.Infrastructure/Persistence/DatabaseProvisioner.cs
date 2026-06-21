using EstateOps.Infrastructure.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql;

namespace EstateOps.Infrastructure.Persistence;

public sealed class DatabaseProvisioner(IOptions<DatabaseOptions> options, ILogger<DatabaseProvisioner> logger)
{
  private const string DuplicateDatabaseSqlState = "42P04";
  private const string InsufficientPrivilegeSqlState = "42501";
  private const string InvalidCatalogNameSqlState = "3D000";

  public async Task EnsureDatabaseExistsAsync(CancellationToken cancellationToken = default)
  {
    var databaseOptions = options.Value;
    var targetBuilder = DatabaseConnectionStringFactory.CreateBuilder(databaseOptions);
    var targetDatabase = targetBuilder.Database;

    if (string.IsNullOrWhiteSpace(targetDatabase))
    {
      logger.LogError("Database auto-creation skipped because no target database name is configured.");
      return;
    }

    if (await CanConnectToTargetDatabaseAsync(targetBuilder.ConnectionString, targetBuilder, cancellationToken))
    {
      return;
    }

    await CreateMissingDatabaseAsync(databaseOptions, targetBuilder, cancellationToken);
  }

  private async Task<bool> CanConnectToTargetDatabaseAsync(
    string targetConnectionString,
    NpgsqlConnectionStringBuilder targetBuilder,
    CancellationToken cancellationToken)
  {
    try
    {
      await using var connection = new NpgsqlConnection(targetConnectionString);
      await connection.OpenAsync(cancellationToken);

      logger.LogInformation(
        "Connected to PostgreSQL database {Database} at {Host}:{Port} as {Username}.",
        targetBuilder.Database,
        targetBuilder.Host,
        targetBuilder.Port,
        targetBuilder.Username);

      return true;
    }
    catch (PostgresException exception) when (exception.SqlState == InvalidCatalogNameSqlState)
    {
      logger.LogInformation(
        "PostgreSQL database {Database} does not exist at {Host}:{Port}; attempting to create it.",
        targetBuilder.Database,
        targetBuilder.Host,
        targetBuilder.Port);

      return false;
    }
    catch (Exception exception)
    {
      logger.LogError(
        exception,
        "Could not connect to PostgreSQL database {Database} at {Host}:{Port} as {Username}. Database auto-creation only runs when the database is missing.",
        targetBuilder.Database,
        targetBuilder.Host,
        targetBuilder.Port,
        targetBuilder.Username);

      return true;
    }
  }

  private async Task CreateMissingDatabaseAsync(
    DatabaseOptions databaseOptions,
    NpgsqlConnectionStringBuilder targetBuilder,
    CancellationToken cancellationToken)
  {
    var targetDatabase = targetBuilder.Database;
    if (string.IsNullOrWhiteSpace(targetDatabase))
    {
      logger.LogError("Database auto-creation skipped because no target database name is configured.");
      return;
    }

    var maintenanceConnectionString = DatabaseConnectionStringFactory.CreateMaintenance(databaseOptions);
    var maintenanceBuilder = new NpgsqlConnectionStringBuilder(maintenanceConnectionString);

    try
    {
      await using var connection = new NpgsqlConnection(maintenanceConnectionString);
      await connection.OpenAsync(cancellationToken);

      if (await DatabaseExistsAsync(connection, targetDatabase, cancellationToken))
      {
        logger.LogInformation("PostgreSQL database {Database} already exists.", targetDatabase);
        return;
      }

      await using var command = connection.CreateCommand();
      command.CommandText = $"CREATE DATABASE {QuoteIdentifier(targetDatabase)}";
      await command.ExecuteNonQueryAsync(cancellationToken);

      logger.LogInformation(
        "Created PostgreSQL database {Database} at {Host}:{Port} as {Username}.",
        targetDatabase,
        targetBuilder.Host,
        targetBuilder.Port,
        targetBuilder.Username);
    }
    catch (PostgresException exception) when (exception.SqlState == DuplicateDatabaseSqlState)
    {
      logger.LogInformation("PostgreSQL database {Database} was created by another process.", targetDatabase);
    }
    catch (PostgresException exception) when (exception.SqlState == InsufficientPrivilegeSqlState)
    {
      logger.LogError(
        exception,
        "Could not create PostgreSQL database {Database}. User {Username} needs the CREATEDB privilege or the database must be created manually.",
        targetDatabase,
        targetBuilder.Username);
    }
    catch (Exception exception)
    {
      logger.LogError(
        exception,
        "Could not create PostgreSQL database {Database} using maintenance database {MaintenanceDatabase} at {Host}:{Port} as {Username}.",
        targetDatabase,
        maintenanceBuilder.Database,
        maintenanceBuilder.Host,
        maintenanceBuilder.Port,
        maintenanceBuilder.Username);
    }
  }

  private static async Task<bool> DatabaseExistsAsync(
    NpgsqlConnection connection,
    string databaseName,
    CancellationToken cancellationToken)
  {
    await using var command = connection.CreateCommand();
    command.CommandText = "SELECT EXISTS (SELECT 1 FROM pg_database WHERE datname = @databaseName)";
    command.Parameters.AddWithValue("databaseName", databaseName);

    var result = await command.ExecuteScalarAsync(cancellationToken);
    return result is true;
  }

  private static string QuoteIdentifier(string identifier)
  {
    return "\"" + identifier.Replace("\"", "\"\"", StringComparison.Ordinal) + "\"";
  }
}
