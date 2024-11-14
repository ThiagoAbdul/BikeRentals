using BikeRentalCore.Data;
using BikeRentalCore.Entities;
using BikeRentalCore.Enums;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalCore.Services;

public class UnityService(AppDbContext db)
{
    public async Task<IEnumerable<Unity>> AddUnits(IEnumerable<Unity> units)
    {
        foreach (var unity in units)
        {
            unity.Status = ERentalStatus.Available;
            unity.Sku = Guid.NewGuid().ToString()[..6];
            await db.Units.AddAsync(unity);      
        }


        await db.SaveChangesAsync();

        return units;
    }


    public Task<Unity?> GetBySkuAsync(string sku)
    {
        return db.Units
            .Include(x => x.Bike)
            .Include(x => x.RentalPoint)
            .Include(x => x.Tenancies
                .Where(y => y.StartedAt != null && y.ReturnDate == null))
            .FirstOrDefaultAsync(u => u.Sku == sku);
    }


}
