namespace EstateOps.Domain.Organizations;

public sealed class Organization
{
  public Guid Id { get; private set; } = Guid.CreateVersion7();

  public required string Name { get; set; }

  public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;
}
