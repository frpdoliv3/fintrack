using FinTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinTrack.Persistence.Contexts;
public partial class FinTrackDbContext
{
    public DbSet<Operation> Operations => Set<Operation>();

    private void SetupOperationConstraints(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Operation>()
            .HasKey(o => o.Id);

        modelBuilder.Entity<Operation>()
            .Property(o => o.OperationType)
            .IsRequired();

        modelBuilder.Entity<Operation>()
            .Property(o => o.OperationDate)
            .IsRequired();

        modelBuilder.Entity<Operation>()
            .Property(o => o.Value)
            .HasColumnType("decimal(19,4)")
            .IsRequired();

        modelBuilder.Entity<Operation>()
            .Property(o => o.Quantity)
            .IsRequired();

        modelBuilder.Entity<Operation>()
            .Property (o => o.ForeignTaxes)
            .HasColumnType("decimal(19,4)")
            .IsRequired();

        modelBuilder.Entity<Operation>()
            .Property(o => o.ExpensesAndCharges)
            .HasColumnType("decimal(19,4)")
            .IsRequired();

        //Security
        modelBuilder.Entity<Security>()
            .HasMany(s => s.Operations)
            .WithOne(o => o.Security)
            .HasForeignKey("SecurityId")
            .IsRequired();
    }
}
