using FinTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinTrack.Persistence.Context
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
        }
    }
}
