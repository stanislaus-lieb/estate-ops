using EstateOps.Domain.Organizations;
using EstateOps.Domain.ReferenceData;
using Microsoft.EntityFrameworkCore;

namespace EstateOps.Infrastructure.Persistence;

public sealed class EstateOpsDbContext(DbContextOptions<EstateOpsDbContext> options) : DbContext(options)
{
  public DbSet<Country> Countries => Set<Country>();

  public DbSet<Organization> Organizations => Set<Organization>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    ArgumentNullException.ThrowIfNull(modelBuilder);

    modelBuilder.Entity<Country>(entity =>
    {
      entity.ToTable("Countries");
      entity.HasKey(country => country.Id);
      entity.Property(country => country.IsoCode).HasMaxLength(2).IsRequired();
      entity.Property(country => country.Name).HasMaxLength(128).IsRequired();
      entity.HasIndex(country => country.IsoCode).IsUnique();
    });

    modelBuilder.Entity<Organization>(entity =>
    {
      entity.ToTable("Organizations");
      entity.HasKey(organization => organization.Id);
      entity.Property(organization => organization.Name).HasMaxLength(160).IsRequired();
      entity.Property(organization => organization.CreatedAt).IsRequired();
    });
  }
}
