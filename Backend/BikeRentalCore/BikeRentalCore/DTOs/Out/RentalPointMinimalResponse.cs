using BikeRentalCore.Entities;

namespace BikeRentalCore.DTOs.Out
{
    public class RentalPointMinimalResponse
    {
        public Guid Id { get; set; }
        public string UnityName { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public int AddressNumber { get; set; }
        public RentalPointMinimalResponse(RentalPoint rentalPoint)
        {
            Id = rentalPoint.Id;
            UnityName = rentalPoint.UnityName;
            ZipCode = rentalPoint.ZipCode;
            AddressNumber = rentalPoint.AddressNumber;
        }

        public override bool Equals(object? obj)
        {
            if(obj is null) return false;

            if(obj is RentalPointMinimalResponse other)
            {
                return Id == other.Id;
            }

            return false;



        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
