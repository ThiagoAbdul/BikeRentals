namespace BikeRentalAuthGateway.Extensions;

public static class GatewayExtensions
{
    public static GatewayConfiguration Gateway(this IConfiguration configuration)
    {
        GatewayConfiguration gateway = new()
        {
            MainApiUrl = Environment
                .GetEnvironmentVariable("MAIN_API_URL")
                ?? "https://localhost:7115"

        };

        return gateway;
    }
}

public class GatewayConfiguration
{
    public string MainApiUrl { get; set; } = string.Empty;
    public GatewayConfiguration()
    {
        
    }
}
