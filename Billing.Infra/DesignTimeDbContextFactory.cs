
using Billing.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
namespace Billing.Infra;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BillingDbContext>
{
    public BillingDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BillingDbContext>();
        optionsBuilder.UseSqlServer("Server=Carol\\SQLEXPRESS;Database=Billing;Trusted_Connection=True;TrustServerCertificate=True;");

        return new BillingDbContext(optionsBuilder.Options);
    }
}
