namespace EstateOps.Infrastructure.Configuration;

public sealed class DatabaseOptions
{
  public const string SectionName = "EstateOps:Database";

  public string Host { get; set; } = "localhost";

  public int Port { get; set; } = 5432;

  public string Database { get; set; } = "estateops";

  public string MaintenanceDatabase { get; set; } = "postgres";

  public string Username { get; set; } = "estateops";

  public string? Password { get; set; }

  public string SslMode { get; set; } = "Prefer";

  public string GssEncryptionMode { get; set; } = "Disable";
}
