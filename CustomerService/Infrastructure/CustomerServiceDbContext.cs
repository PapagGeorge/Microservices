using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Infrastructure
{
    public class CustomerServiceDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public CustomerServiceDbContext(DbContextOptions<CustomerServiceDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany(o => o.Addresses)
                .WithOne(p => p.Customer)
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Address>()
                .HasOne(p => p.Customer)
                .WithMany(o => o.Addresses)
                .HasForeignKey(p => p.CustomerId);
        }
    }
}
