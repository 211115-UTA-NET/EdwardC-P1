using Microsoft.EntityFrameworkCore;

namespace Project_1.Api.Models
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options)
            : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; } = null!;
    }
}
