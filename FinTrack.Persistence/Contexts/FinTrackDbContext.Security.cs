using FinTrack.Domain.Entities;
using FinTrack.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace FinTrack.Persistence.Contexts;

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
        
        // CounterpartyCountry
        modelBuilder.Entity<Security>()
            .HasOne(s => s.CounterpartyCountry)
            .WithMany()
            .HasForeignKey("CounterpartyCountryId")
            .IsRequired(false);

        // SourceCountry
        modelBuilder.Entity<Security>()
            .HasOne(s => s.SourceCountry)
            .WithMany()
            .HasForeignKey("SourceCountryId")
            .IsRequired(false);
        
        // Ownership
        modelBuilder.Entity<EFUser>()
            .HasMany<Security>()
            .WithOne()
            .HasForeignKey("OwnerId")
            .IsRequired();
    }
}
