using BikeRentalCore.Entities;

namespace BikeRentalCore.DTOs.Out;

public class BrandResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public BrandResponse(Brand brand)
    {
        Id = brand.Id;
        Name = brand.Name;

    }
}
