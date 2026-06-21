using Npgsql;

namespace EstateOps.Infrastructure.Configuration;

public static class DatabaseConnectionStringFactory
{
  public static string Create(DatabaseOptions options)
  {
    return CreateBuilder(options).ConnectionString;
  }

  public static string CreateMaintenance(DatabaseOptions options)
  {
    var builder = CreateBuilder(options);
    builder.Database = ReadEnvironment("ESTATEOPS_DB_MAINTENANCE_DATABASE", options.MaintenanceDatabase);

    return builder.ConnectionString;
  }

  public static NpgsqlConnectionStringBuilder CreateBuilder(DatabaseOptions options)
  {
    ArgumentNullException.ThrowIfNull(options);

    return new NpgsqlConnectionStringBuilder
    {
      Host = ReadEnvironment("ESTATEOPS_DB_HOST", options.Host),
      Port = ReadEnvironmentInt("ESTATEOPS_DB_PORT", options.Port),
      Database = ReadEnvironment("ESTATEOPS_DB_NAME", options.Database),
      Username = ReadEnvironment("ESTATEOPS_DB_USERNAME", options.Username),
      SslMode = Enum.TryParse<SslMode>(ReadEnvironment("ESTATEOPS_DB_SSL_MODE", options.SslMode), ignoreCase: true, out var sslMode)
        ? sslMode
        : SslMode.Prefer,
      GssEncryptionMode = Enum.TryParse<GssEncryptionMode>(ReadGssEncryptionMode(options.GssEncryptionMode), ignoreCase: true, out var gssEncryptionMode)
        ? gssEncryptionMode
        : GssEncryptionMode.Disable,
      IncludeErrorDetail = false,
      Password = ReadEnvironment("ESTATEOPS_DB_PASSWORD", options.Password),
    };
  }

  private static string ReadEnvironment(string key, string? fallback)
  {
    return Environment.GetEnvironmentVariable(key) ?? fallback ?? string.Empty;
  }

  private static int ReadEnvironmentInt(string key, int fallback)
  {
    var value = Environment.GetEnvironmentVariable(key);
    return int.TryParse(value, out var parsed) ? parsed : fallback;
  }

  private static string ReadGssEncryptionMode(string fallback)
  {
    return Environment.GetEnvironmentVariable("ESTATEOPS_DB_GSS_ENCRYPTION_MODE")
      ?? Environment.GetEnvironmentVariable("ESTATEOPS_DB_GSS_ENC_MODE")
      ?? fallback;
  }
}
