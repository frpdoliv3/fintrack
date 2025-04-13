using FinTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinTrack.Infrastructure.Contexts;

public partial class FinTrackDbContext
{
    public DbSet<Country> Countries => Set<Country>();

    private void SetupCountryConstraints(ModelBuilder modelBuilder)
    {
        // Id
        modelBuilder.Entity<Country>()
            .HasKey(c => c.Id);

        // Name
        modelBuilder.Entity<Country>()
            .Property(c => c.Name)
            .IsRequired();
        modelBuilder.Entity<Country>()
            .HasIndex(c => c.Name)
            .IsUnique();

        // Alpha2Code
        modelBuilder.Entity<Country>()
            .Property(c => c.Alpha2Code)
            .IsFixedLength()
            .IsRequired()
            .HasMaxLength(2);
        modelBuilder.Entity<Country>()
            .HasIndex(c => c.Alpha2Code)
            .IsUnique();

        // Alpha3Code
        modelBuilder.Entity<Country>()
            .Property(c => c.Alpha3Code)
            .IsFixedLength()
            .IsRequired()
            .HasMaxLength(3);
        modelBuilder.Entity<Country>()
            .HasIndex(c => c.Alpha3Code)
            .IsUnique();
    }
}
