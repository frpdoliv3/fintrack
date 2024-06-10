using FinTrack.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace FinTrack.Server.Data
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Security> Securities { get; set; }
        public virtual DbSet<SecurityTransaction> SecurityTransactions { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
