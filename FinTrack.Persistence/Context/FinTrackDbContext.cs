using FinTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinTrack.Persistence.Context
{
    public partial class FinTrackDbContext : DbContext
    {
        public FinTrackDbContext(DbContextOptions<FinTrackDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetupCountryConstraints(modelBuilder);
            SetupCurrencyConstraints(modelBuilder);
            //SetupSecurityConstraints(modelBuilder);
        }
    }
}
