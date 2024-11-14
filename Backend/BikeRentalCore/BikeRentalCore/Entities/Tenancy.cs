using BikeRentalCore.DTOs.In;

namespace BikeRentalCore.Entities;

public class Tenancy : EntityBase
{
    public Guid UnityId { get; set; }
    public Unity? Unity { get; set; }
    public string UserId { get; set; } = string.Empty;
    public DateOnly? StartedAt { get; set; }
    public DateOnly? ReturnDate { get; set; }

    public int RentedDays { get; set; }
    public string? RentalCode { get; set; }


    public static Tenancy Create(string userId, Unity unity, int rentedDays)
    {
        return new Tenancy()
        {
            CreatedAt = DateTime.UtcNow,
            CreatedBy = userId,
            Unity = unity,
            UserId = userId,
            Deleted = false,
            RentedDays = rentedDays,
        };
    }

    public void GenerateRentalCode()
    {
        RentalCode = Guid.NewGuid().ToString()[..6];
    }

}
