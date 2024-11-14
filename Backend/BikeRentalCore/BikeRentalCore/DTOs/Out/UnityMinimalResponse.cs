using BikeRentalCore.Entities;

namespace BikeRentalCore.DTOs.Out;

public class UnityMinimalResponse
{
    public Guid Id { get; set; }
    public Guid BikeId { get; set; }
    public string Sku { get; set; }
    public Guid RentalPointId { get; set; }
    public string RentalStatus { get; set; }

    public UnityMinimalResponse(Unity unity)
    {
        Id = unity.Id;
        BikeId = unity.BikeId;
        Sku = unity.Sku;
        RentalPointId = unity.RentalPointId;
        RentalStatus = unity.Status.ToString();
    }
}
