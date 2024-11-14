using BikeRentalCore.Enums;

namespace BikeRentalCore.DTOs.In;

public record RentRequest(Guid BikeId, 
                          Guid PointId, 
                          PaymentMethod PaymentMethod, 
                          string PaymentData,
                          int RentedDays);
