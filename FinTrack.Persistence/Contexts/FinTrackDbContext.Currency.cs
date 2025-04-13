using FinTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinTrack.Infrastructure.Contexts;

public partial class FinTrackDbContext
{
    public DbSet<Currency> Currencies => Set<Currency>();

    private void SetupCurrencyConstraints(ModelBuilder modelBuilder)
    {
        // Id
        modelBuilder.Entity<Currency>()
            .HasKey(c => c.Id);

        // Name
        modelBuilder.Entity<Currency>()
            .Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();
        modelBuilder.Entity<Currency>()
            .HasIndex(c => c.Name)
            .IsUnique();

        // Alpha3Code
        modelBuilder.Entity<Currency>()
            .Property(c => c.Alpha3Code)
            .HasMaxLength(3)
            .IsFixedLength()
            .IsRequired();
        modelBuilder.Entity<Currency>()
            .HasIndex(c => c.Alpha3Code)
            .IsUnique();

        // Symbol
        modelBuilder.Entity<Currency>()
            .Property(c => c.Symbol)
            .HasMaxLength(10);

        // Decimals
        modelBuilder.Entity<Currency>()
            .Property(c => c.Decimals)
            .HasColumnType("tinyint")
            .IsRequired();

        // NumberToMajor
        modelBuilder.Entity<Currency>()
            .Property(c => c.NumberToMajor)
            .HasColumnType("tinyint")
            .IsRequired();
    }
}
