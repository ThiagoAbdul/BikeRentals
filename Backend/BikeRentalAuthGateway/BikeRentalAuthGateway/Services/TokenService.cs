using BikeRentalAuthGateway.Entities;
using BikeRentalAuthGateway.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BikeRentalAuthGateway.Services;

public class TokenService(JsonWebTokenHelper jwtHelper)
{
    public string GenerateAccessToken(User user)
    {
        IEnumerable<Claim> claims =
            [
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email),
                new(JwtRegisteredClaimNames.Name, user.FullName),
                new("Role", user.Role.ToString())
            ];
        return jwtHelper.Create(claims, 1);
    }

    public string GenerateRefreshToken(User user, string accesssToken)
    {
        IEnumerable<Claim> claims =
            [
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new("access", accesssToken)
            ];
        return jwtHelper.Create(claims, 24);
    }
}
