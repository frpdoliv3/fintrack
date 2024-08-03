using FinTrack.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinTrack.Server.Data;

public class AppDbContext : IdentityDbContext<User>
{
    public virtual DbSet<Security> Securities { get; set; }
    public virtual DbSet<SecurityTransaction> SecurityTransactions { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        var admin = new User
        {
            Id = "1cd2cae2-21c3-453b-950e-9f9303bf5e9e",
            UserName = "fpoliveira",
            Email = "fpoliveira@example.com",
            EmailConfirmed = true,
            NormalizedUserName = "FPOLIVEIRA",
            NormalizedEmail = "FPOLIVEIRA@EXAMPLE.COM"
        };
        PasswordHasher<User> ph = new PasswordHasher<User>();
        admin.PasswordHash = ph.HashPassword(admin, "my_password");
        builder.Entity<User>().HasData(admin);
    }
}

