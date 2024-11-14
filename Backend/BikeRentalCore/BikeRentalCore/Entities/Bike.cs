using BikeRentalCore.Enums;

namespace BikeRentalCore.Entities;

public class Bike : EntityBase
{

    public EBikeType BikeType { get; set; }
    public EFrameMaterial FrameMaterial { get; set; }
    public EFrameSize FrameSize { get; set; }
    public float WheelSize { get; set; }
    public int NumberOfGears { get; set; }
    public ESuspensionType SuspensionType { get; set; }
    public ETransmissionType TransmissionType { get; set; }
    public ECommonColor Color { get; set; }
    public Guid BrandId { get; set; }
    public Brand? Brand { get; set; }
    public Decimal BasePrice { get; set; }
    public EBrakeType BrakeType { get; set; }
    public virtual List<Unity> Units { get; set; } = [];
    public string ImagePath { get; set; } = string.Empty;

    public Bike()
    {
        
    }

    public Bike(EBikeType bikeType, EFrameMaterial frameMaterial, EFrameSize frameSize, float wheelSize, int numberOfGears, ESuspensionType suspensionType, ETransmissionType transmissionType, ECommonColor color, Guid brandId, decimal basePrice, EBrakeType brakeType)
    {
        BikeType = bikeType;
        FrameMaterial = frameMaterial;
        FrameSize = frameSize;
        WheelSize = wheelSize;
        NumberOfGears = numberOfGears;
        SuspensionType = suspensionType;
        TransmissionType = transmissionType;
        Color = color;
        BrandId = brandId;
        BasePrice = basePrice;
        BrakeType = brakeType;
    }
}
