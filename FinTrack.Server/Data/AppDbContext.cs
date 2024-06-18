using FinTrack.Server.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinTrack.Server.Data
{
    public class AppDbContext : IdentityDbContext<UserIdentity>
    {
        public virtual DbSet<Security> Securities { get; set; }
        public virtual DbSet<SecurityTransaction> SecurityTransactions { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
