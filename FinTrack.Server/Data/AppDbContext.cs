using FinTrack.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace FinTrack.Server.Data
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Security> Securities { get; set; }
        public virtual DbSet<SecurityTransaction> SecurityTransactions { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Security>()
                .HasMany(e => e.Transactions)
                .WithOne(e => e.Security)
                .HasForeignKey("security_id")
                .IsRequired();
        }
    }
}
