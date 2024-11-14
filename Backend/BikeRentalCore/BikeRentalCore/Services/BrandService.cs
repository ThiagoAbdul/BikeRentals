using BikeRentalCore.Data;
using BikeRentalCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalCore.Services;

public class BrandService(AppDbContext db)
{
    public async Task<IEnumerable<Brand>> GetAllBrands()
    {
        return await db.Brands.AsNoTracking()
            .Where(x => !x.Deleted)
            .ToListAsync();
    }

    public async Task<Brand?> GetBrandByIdAsync(Guid id)
    {
        return await db.Brands
            .Include(x => x.Bikes)
            .ThenInclude(x => x.Units)
            .Where(x => x.Bikes.Any(x => x.Units.Count > 0))
            .FirstOrDefaultAsync(x => x.Id == id && !x.Deleted);
    }
}
