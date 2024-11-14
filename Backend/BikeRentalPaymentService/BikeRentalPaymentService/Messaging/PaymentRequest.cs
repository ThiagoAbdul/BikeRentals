using BikeRentalPaymentService.Models;

namespace Messaging;

public record PaymentRequest(string ObjectId, 
                            string UserId, 
                            PaymentMethod PaymentMethod, 
                            string PaymentData);