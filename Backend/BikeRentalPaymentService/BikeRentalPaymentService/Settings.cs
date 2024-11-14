namespace BikeRentalPaymentService;

public class Settings(IConfiguration configuration)
{
    public string PaymentGatewayUrl 
    { 
        get => Environment.GetEnvironmentVariable("PAYMENT_GATEWAY_URL") 
               ?? "https://localhost:7210/payment"; 
    }

    public string HostAddress 
    { 
        get => Environment.GetEnvironmentVariable("HOST_ADDRESS")
               ?? "https://localhost:7199";
    }

}
