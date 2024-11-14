using BikeRentalCore.Enums;

namespace Messaging;

public record PaymentRequest(string ObjectId, string UserId, PaymentMethod PaymentMethod, string PaymentData);