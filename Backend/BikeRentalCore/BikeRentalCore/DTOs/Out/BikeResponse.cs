using BikeRentalCore.Entities;
using BikeRentalCore.Enums;

namespace BikeRentalCore.DTOs.Out
{
    public class BikeResponse
    {
        public Guid Id { get; set; }
        public EBikeType BikeType { get; set; }
        public EFrameMaterial FrameMaterial { get; set; }
        public EFrameSize FrameSize { get; set; }
        public float WheelSize { get; set; }
        public int NumberOfGears { get; set; }
        public ESuspensionType SuspensionType { get; set; }
        public ETransmissionType TransmissionType { get; set; }
        public ECommonColor Color { get; set; }
        public Guid BrandId { get; set; }
        public BrandResponse? Brand { get; set; }
        public Decimal BasePrice { get; set; }
        public EBrakeType BrakeType { get; set; }
        //public virtual List<UnityMinimalResponse> Units { get; set; } = [];
        public string ImagePath { get; set; }
        public int Amount { get; set; }
        public IEnumerable<RentalPointMinimalResponse> RentalPoints { get; set; }

        public BikeResponse(Bike bike)
        {
            Id = bike.Id;
            BikeType = bike.BikeType;
            FrameMaterial = bike.FrameMaterial;
            FrameSize = bike.FrameSize;
            WheelSize = bike.WheelSize;
            NumberOfGears = bike.NumberOfGears;
            SuspensionType = bike.SuspensionType;
            TransmissionType = bike.TransmissionType;
            Color = bike.Color;
            BrandId = bike.BrandId;
            if(bike.Brand != null)
                Brand = new BrandResponse(bike.Brand);
            BasePrice = bike.BasePrice;
            BrakeType = bike.BrakeType;
            //Units = bike.Units.Select(x => new UnityMinimalResponse(x)).ToList();
            ImagePath = bike.ImagePath;
            Amount = bike.Units.Count;
            RentalPoints = bike.Units   
                .Where(x => x.RentalPoint is not null)
                .Select(x => new RentalPointMinimalResponse(x.RentalPoint!))
                .Distinct();
        }
    }
}
