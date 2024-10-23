using FinTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinTrack.Persistence.Contexts
{
    public partial class FinTrackDbContext
    {
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
                .Property(s => s.NativeCurrency)
                .IsRequired();

            // CounterpartyCountry
            modelBuilder.Entity<Security>()
                .Property(s => s.CounterpartyCountry)
                .IsRequired();
        }
    }
}
