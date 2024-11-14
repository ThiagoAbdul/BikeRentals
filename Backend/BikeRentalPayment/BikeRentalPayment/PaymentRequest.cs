namespace BikeRentalPayment;

public record PaymentRequest(string Data, int PaymentMethod, string CallbackUrl);