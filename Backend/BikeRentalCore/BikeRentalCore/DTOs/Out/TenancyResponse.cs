using BikeRentalCore.Entities;

namespace BikeRentalCore.DTOs.Out;

public class TenancyResponse
{
    public Guid Id { get; set; }
    public Guid UnityId { get; set; }
    public UnityMinimalResponse? Unity { get; set; }
    public string UserId { get; set; } = string.Empty;
    public DateOnly? StartedAt { get; set; }
    public int RentedDays { get; set; }
    public string? RentalCode { get; set; }

    public TenancyResponse(Tenancy tenancy)
    {
        Id = tenancy.Id;
        UnityId = tenancy.UnityId;
        if (tenancy.Unity is not null)
            Unity = new UnityMinimalResponse(tenancy.Unity);
        UserId = tenancy.UserId;
        StartedAt = tenancy.StartedAt;
        RentedDays = tenancy.RentedDays;
        RentalCode = tenancy.RentalCode;
    }
}
