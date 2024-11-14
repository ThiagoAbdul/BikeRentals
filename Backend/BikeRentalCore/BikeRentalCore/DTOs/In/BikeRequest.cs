using BikeRentalCore.Entities;
using BikeRentalCore.Enums;

namespace BikeRentalCore.DTOs.In;

public class BikeRequest
{
    public Guid? Id { get; set; }

    public EBikeType BikeType { get; set; }
    public EFrameMaterial FrameMaterial { get; set; }
    public EFrameSize FrameSize { get; set; }
    public float WheelSize { get; set; }
    public int NumberOfGears { get; set; }
    public ESuspensionType SuspensionType { get; set; }
    public ETransmissionType TransmissionType { get; set; }
    public ECommonColor Color { get; set; }
    public Guid BrandId { get; set; }
    public Decimal BasePrice { get; set; }
    public EBrakeType BrakeType { get; set; }
    public IFormFile Image { get; set; }


    public BikeRequest()
    {
        
    }

    public Bike ToEntity()
    {
        return new Bike(BikeType, FrameMaterial, FrameSize, WheelSize, NumberOfGears, 
            SuspensionType, TransmissionType, Color, BrandId, BasePrice, BrakeType);
    }

}
