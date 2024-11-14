using BikeRentalCore.Data;
using BikeRentalCore.Entities;
using BikeRentalCore.Enums;
using BikeRentalCore.Message;
using BikeRentalCore.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalCore.Services;

public class TenancyService(AppDbContext db)
{
    public async Task<Tenancy?> ChangeRentalStatus(Guid id, ERentalStatus rentalStatus)
    {
        Tenancy? tenancy = await db.Tenancies
            .Include(x => x.Unity)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (tenancy is null) return null;

        tenancy.Unity!.Status = rentalStatus;

        if(rentalStatus == ERentalStatus.Rented)
        {
            tenancy.StartedAt = DateOnly.FromDateTime(DateTime.UtcNow);
            tenancy.GenerateRentalCode();
        }

        db.Update(tenancy);
        await db.SaveChangesAsync();

        return tenancy;
    }

    public Task<Tenancy?> GetByCodeRentalAsync(string code)
    {
        return db.Tenancies.FirstOrDefaultAsync(x => x!.Deleted && x.RentalCode == code);
    }

    public async Task<Result> EndLease(Guid tenancyId)
    {
        Tenancy? tenancy = await db.Tenancies
            .FirstOrDefaultAsync(x =>x.Id == tenancyId);

        if(tenancy is null) 
            return Result.SummaryError("Not found");

        if (tenancy.ReturnDate is not null)
            return Result.SummaryError("Lease already completed");


        tenancy.ReturnDate = DateOnly.FromDateTime(DateTime.UtcNow);

        db.Tenancies.Update(tenancy);

        await db.SaveChangesAsync();

        return Result.Empty();
        

    }
}
