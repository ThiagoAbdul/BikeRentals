using BikeRentalCore.Entities;
using BikeRentalCore.Enums;

namespace BikeRentalCore.DTOs.Out;

public class UnityResponse
{
    public Guid Id { get; set; }
    public Guid BikeId { get; set; }
    public BikeResponse? Bike { get; set; }
    public string Sku { get; set; }
    public Guid RentalPointId { get; set; }
    public RentalPointMinimalResponse? RentalPoint { get; set; }
    public ERentalStatus Status { get; set; }
    public TenancyMinimalResponse? ActiveTenancy { get; set; }

    public UnityResponse(Unity unity)
    {
        Id = unity.Id;
        BikeId = unity.BikeId;

        if(unity.Bike is not null)
            Bike = new BikeResponse(unity.Bike);

        Sku = unity.Sku;
        RentalPointId = unity.RentalPointId;

        if (unity.RentalPoint is not null)
            RentalPoint = new RentalPointMinimalResponse(unity.RentalPoint);

        Status = unity.Status;

        Tenancy? tenancy = unity.Tenancies.FirstOrDefault();
        if(tenancy is not null)
            ActiveTenancy = new TenancyMinimalResponse(tenancy);

    }
}
