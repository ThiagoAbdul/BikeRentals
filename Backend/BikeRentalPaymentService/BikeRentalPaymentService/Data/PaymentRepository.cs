using BikeRentalPaymentService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BikeRentalPaymentService.Data;

public class PaymentRepository(AppDbContext db)
{
    public async Task<Payment> CreateAsync(Payment payment)
    {
        await db.Payments.AddAsync(payment);
        await db.SaveChangesAsync();
        return payment;
    }

    public Task<IDbContextTransaction> BeginTransaction()
    {
        return db.Database.BeginTransactionAsync();
        
    }

    public Task<Payment?> GetByIdAsync(Guid id)
    {
        return db.Payments
            .Where(x => !x.Deleted)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Payment>> GetPaymentsByUserId(string userId)
    {
        return await db.Payments
            .Where(x => x.UserId == userId && !x.Deleted)
            .ToListAsync();
    }

    public async Task<Payment> UpdateAsync(Payment payment)
    {
        db.Payments.Update(payment);
        await db.SaveChangesAsync();
        return payment;
    }



    

}
