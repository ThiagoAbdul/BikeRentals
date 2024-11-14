namespace BikeRentalAuthGateway.DTOs.Out;

public record SignInResponse(Guid UserId, string AccessToken, string RefreshToken);
