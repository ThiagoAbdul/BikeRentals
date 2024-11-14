using BikeRentalAuthGateway.Entities;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalAuthGateway.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(e =>
        {
            e.HasKey(e => e.Id);
            e.HasIndex(e => e.Email);
            e.HasQueryFilter(x => !x.Deleted);
        });
    }
}
