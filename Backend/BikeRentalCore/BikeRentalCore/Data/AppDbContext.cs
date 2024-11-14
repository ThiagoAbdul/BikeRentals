using BikeRentalCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalCore.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Bike> Bikes { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<RentalPoint> RentalPoints { get; init; }
    public DbSet<Tenancy> Tenancies { get; set; }
    public DbSet<Unity> Units { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Bike>(e =>
        { 
            e.HasKey(x => x.Id);
            e.HasOne(x => x.Brand).WithMany(x => x.Bikes).HasForeignKey(x => x.BrandId);
        });

        builder.Entity<Brand>(e =>
        {
            e.HasKey(x => x.Id);

        });

        builder.Entity<RentalPoint>(e =>
        {
            e.HasKey(x =>x.Id);

        });

        builder.Entity<Tenancy>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasOne(x => x.Unity).WithMany().HasForeignKey(x => x.UnityId);

        });

        builder.Entity<Unity>(e =>
        {
            e.HasOne(x => x.RentalPoint).WithMany(x => x.Units).HasForeignKey(x => x.RentalPointId);
            e.HasOne(x => x.Bike).WithMany(x => x.Units).HasForeignKey(x => x.BikeId);
            e.Property(x => x.Sku).ValueGeneratedOnAdd();
            e.HasMany(x => x.Tenancies).WithOne(x => x.Unity);
            

        });
    }
}
