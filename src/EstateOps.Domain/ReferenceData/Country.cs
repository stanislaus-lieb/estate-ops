namespace EstateOps.Domain.ReferenceData;

public sealed class Country
{
  public Guid Id { get; private set; } = Guid.CreateVersion7();

  public required string IsoCode { get; set; }

  public required string Name { get; set; }
}
