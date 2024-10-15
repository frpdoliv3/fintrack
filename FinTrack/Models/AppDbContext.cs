using Microsoft.EntityFrameworkCore;

namespace FinTrack.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Security> Securities => Set<Security>();
        public DbSet<Country> Countries => Set<Country>();

        public AppDbContext(DbContextOptions opts) : base(opts) { }
    }
}
