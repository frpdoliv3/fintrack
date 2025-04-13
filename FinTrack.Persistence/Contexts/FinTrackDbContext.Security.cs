using FinTrack.Domain.Entities;
using FinTrack.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FinTrack.Infrastructure.Contexts;

public partial class FinTrackDbContext
{
    public DbSet<Security> Securities => Set<Security>();
    
    private void SetupSecurityConstraints(ModelBuilder modelBuilder)
    {
        // Id
        modelBuilder.Entity<Security>()
            .HasKey(s => s.Id);

        // Name
        modelBuilder.Entity<Security>()
            .Property(s => s.Name)
            .IsRequired();
        
        // Isin
        modelBuilder.Entity<Security>()
            .Property(s => s.Isin)
            .HasMaxLength(12)
            .IsFixedLength();

        // NativeCurrency
        modelBuilder.Entity<Security>()
            .HasOne(s => s.NativeCurrency)
            .WithMany()
            .HasForeignKey("NativeCurrencyId")
            .IsRequired();

        modelBuilder.Entity<Security>()
            .Navigation(s => s.NativeCurrency)
            .AutoInclude();
        
        // CounterpartyCountry
        modelBuilder.Entity<Security>()
            .HasOne(s => s.CounterpartyCountry)
            .WithMany()
            .HasForeignKey("CounterpartyCountryId")
            .IsRequired(false);

        modelBuilder.Entity<Security>()
            .Navigation(s => s.CounterpartyCountry)
            .AutoInclude();
        
        // SourceCountry
        modelBuilder.Entity<Security>()
            .HasOne(s => s.SourceCountry)
            .WithMany()
            .HasForeignKey("SourceCountryId")
            .IsRequired(false);
        
        modelBuilder.Entity<Security>()
            .Navigation(s => s.SourceCountry)
            .AutoInclude();
        
        // Ownership
        modelBuilder.Entity<EFUser>()
            .HasMany<Security>()
            .WithOne()
            .HasForeignKey("OwnerId")
            .IsRequired();
    }
}
