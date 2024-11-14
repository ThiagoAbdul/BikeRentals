using BikeRentalAuthGateway.Data;
using BikeRentalAuthGateway.Entities;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalAuthGateway.Services;

public class UserService(AppDbContext dbContext)
{
    public Task<User?> GetByEmailAsync(string email)
    {
        return dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User> CreateAsync(User user)
    {
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
        return user;
    }

    public Task<User?> GetByIdAsync(Guid id)
    {
        return dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
    }
}
