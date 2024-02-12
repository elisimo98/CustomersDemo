using Customers.Api.Aggregates.Customer.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace Customers.Api.Data
{
    // Add-Migration initialMigration
    // update-database
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<CustomerEntity> Customers { get; set; }
    }
}
