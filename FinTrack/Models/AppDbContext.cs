using Microsoft.EntityFrameworkCore;

namespace FinTrack.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Security> Securities { get; set; }

        public AppDbContext(DbContextOptions opts) : base(opts) { }
    }
}
