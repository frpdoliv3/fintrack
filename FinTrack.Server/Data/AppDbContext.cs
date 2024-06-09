using Microsoft.EntityFrameworkCore;

namespace FinTrack.Server.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }
    }
}
