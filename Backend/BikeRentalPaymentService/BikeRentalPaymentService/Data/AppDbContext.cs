using BikeRentalPaymentService.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalPaymentService.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{

    public DbSet<Payment> Payments { get; set; }

    protected void OnModelCreate(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Payment>(e =>
        {
            e.HasKey(x => x.Id);
        });
    }
}
