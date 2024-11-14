using BikeRentalPaymentService.Models;

namespace BikeRentalPaymentService.DTOs;

public record PaymentGatewayRequest(string Data, PaymentMethod PaymentMethod, string CallbackUrl);