namespace BikeRentalAuthGateway.Extensions;

public static class ApiKeyExtension
{
    public static string? ApiKey(this HttpRequest request)
    {
        return request.Headers["Api-Key"];
    }

}
