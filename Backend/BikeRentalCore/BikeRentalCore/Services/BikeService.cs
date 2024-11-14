using BikeRentalCore.Clients;
using BikeRentalCore.Data;
using BikeRentalCore.DTOs.In;
using BikeRentalCore.Entities;
using BikeRentalCore.Enums;
using BikeRentalCore.Message;
using BikeRentalCore.Models;
using MassTransit;
using Messaging;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalCore.Services;

public class BikeService(AppDbContext db, IStorageClient storageClient, IPublishEndpoint publishEndpoint)
{

    public async Task<Page<Bike>> GetBikesPaginated(int pageIndex, int pageSize)
    {
        List<Bike> bikes = await db.Bikes
            .Include(x => x.Brand)
            .Include(x => x.Units.Where(y => y.Status == ERentalStatus.Available))
            .ThenInclude(x => x.RentalPoint)
            .Where(x => !x.Deleted)
            .AsNoTracking()
            .Skip(pageSize * (pageIndex - 1))
            .Take(pageSize)
            .ToListAsync();

        int total = await db.Bikes.Where(x => !x.Deleted).CountAsync();

        return new Page<Bike>(bikes.Where(x => x.Units.Count > 0), pageIndex, pageSize,total);


    }

    public async Task<Bike> CreateAsync(Bike bike, IFormFile file)
    {
        var transaction = await db.Database.BeginTransactionAsync();
        await db.Bikes.AddAsync(bike);
        await db.SaveChangesAsync();

        try
        {
            string filePath = await storageClient.UploadFileAsync(file, bike.Id.ToString());
            bike.ImagePath = filePath;
            await db.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }

        return bike;

    }

    public async Task<Bike?> GetBikeByIdAsync(Guid id)
    {
        return await db.Bikes
            .Include(x => x.Brand)
            .Include(x => x.Units)
            .ThenInclude(x => x.RentalPoint)
            .FirstOrDefaultAsync(x => x.Id == id && !x.Deleted);
    }


    public async Task<Result> RentBikeAsync(string userId, RentRequest rentRequest)
    {
        Unity? unity = await db.Units
            .FirstOrDefaultAsync(
                x => x.BikeId == rentRequest.BikeId
                && x.RentalPointId == rentRequest.PointId
                && x.Status == ERentalStatus.Available);
        if(unity is null)
            return Result.SummaryError("None unity available");

        unity.Status = ERentalStatus.InPaymentProcess;

        Tenancy tenancy = Tenancy.Create(userId, unity, rentRequest.RentedDays);

        await db.Tenancies.AddAsync(tenancy);

        PaymentRequest paymentRequest = new(
                    tenancy.Id.ToString(), 
                    userId, 
                    rentRequest.PaymentMethod, 
                    rentRequest.PaymentData
        );

        await publishEndpoint.Publish(paymentRequest);

        await db.SaveChangesAsync();
        return Result.Empty();
    }


}
