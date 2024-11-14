using System.IdentityModel.Tokens.Jwt;

namespace BikeRentalCore.Extensions;

public static class JwtExtensions
{
    private static readonly JwtSecurityTokenHandler handler = new();
    public static string? JwtInHeader(this HttpRequest request)
    {
        string? authorizationHeader = request.Headers.Authorization;
        if (string.IsNullOrEmpty(authorizationHeader))
            return null;
        return authorizationHeader.Split("Bearer ").ElementAtOrDefault(1);

    }

    public static string? UserId(this HttpRequest request)
    {
        string? token = request.JwtInHeader();
        if (token == null) return null;

        if (handler.ReadToken(token) is not JwtSecurityToken jsonToken) 
            return null;


        return jsonToken.Subject;



    }
}
