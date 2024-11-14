using BikeRentalCore.Data;
using BikeRentalCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalCore.Services;

public class RentalPointService(AppDbContext db)
{

    public async Task<IEnumerable<RentalPoint>> GetAllRentalPoints()
    {
        return await db.RentalPoints.AsNoTracking()
            .Where(x => !x.Deleted)
            .ToListAsync();
    }

    public async Task<RentalPoint?> GetRentalPointByIdAsync(Guid id)
    {
        return await db.RentalPoints
            .Include(x => x.Units)
            .ThenInclude(x => x.Bike)
            .ThenInclude(x => x.Brand)
            .FirstOrDefaultAsync(x => x.Id == id && !x.Deleted);
    }

}
