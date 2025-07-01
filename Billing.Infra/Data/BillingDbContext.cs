using Billing.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Billing.Infra.Data;

public class BillingDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<BillingInformation> Billings { get; set; }
    public DbSet<BillingLine> BillingLines { get; set; }

    public BillingDbContext(DbContextOptions<BillingDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BillingInformation>()
            .HasMany(b => b.Lines)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);  

        modelBuilder.Entity<Customer>()
                    .HasIndex(c => c.Email)
                    .IsUnique();
    }
}