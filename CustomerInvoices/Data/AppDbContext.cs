using Microsoft.EntityFrameworkCore;

namespace CustomerInvoices.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>()
                .HasKey(i => new { i.InvoiceId });

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Customer)
                .WithMany(c => c.Invoices)
                .HasForeignKey(i => i.CustomerId);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Service)
                .WithMany(s => s.Invoices)
                .HasForeignKey(i => i.ServiceId);
        }
    }
}
