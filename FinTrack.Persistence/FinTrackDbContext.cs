using FinTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinTrack.Persistence
{
    public class FinTrackDbContext: DbContext
    {
        public FinTrackDbContext(DbContextOptions<FinTrackDbContext> options) : base(options) 
        {}

        public DbSet<Country> Countries => Set<Country>();
    }
}
