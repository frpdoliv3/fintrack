using FinTrack.Server.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinTrack.Server.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public virtual DbSet<Security> Securities { get; set; }
        public virtual DbSet<SecurityTransaction> SecurityTransactions { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
