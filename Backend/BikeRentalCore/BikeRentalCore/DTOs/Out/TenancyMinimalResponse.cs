using BikeRentalCore.Entities;

namespace BikeRentalCore.DTOs.Out;

public class TenancyMinimalResponse(Tenancy tenancy)
{
    public Guid Id { get; set; } = tenancy.Id;
    public Guid UnityId { get; set; } = tenancy.UnityId;
    public string UserId { get; set; } = tenancy.UserId;
    public DateOnly? StartedAt { get; set; } = tenancy.StartedAt;
    public int RentedDays { get; set; } = tenancy.RentedDays;
    public string? RentalCode { get; set; } = tenancy.RentalCode;
}
