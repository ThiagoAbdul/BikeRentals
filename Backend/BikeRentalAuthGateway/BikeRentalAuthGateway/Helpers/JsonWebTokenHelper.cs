﻿using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace BikeRentalAuthGateway.Helpers;

public sealed class JsonWebTokenHelper(IConfiguration configuration)
{
    private static readonly JsonWebTokenHandler _tokenHandler = new();
    private static readonly string _issuer = "iface-authentication-service";
    private static readonly string _audience = "iface-authentication-service";

    public string Create(IEnumerable<Claim> claims, int expirationHours)
    {

        SigningCredentials credentials = GetSigningCredentials();
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(expirationHours),
            SigningCredentials = credentials,
            Issuer = _issuer,
            Audience = _audience
        };

        return _tokenHandler.CreateToken(tokenDescriptor);
    }

    private SigningCredentials GetSigningCredentials()
    {
        SymmetricSecurityKey securityKey = GetSecurityKey(configuration);
        return new(securityKey, SecurityAlgorithms.HmacSha256);
    }

    public async Task<TokenValidationResult> ValidationTokenResultAsync(string token)
    {
        var result = await _tokenHandler.ValidateTokenAsync(token, GetTokenValidationParameters());

        return result;
    }

    private TokenValidationParameters GetTokenValidationParameters()
    {
        return GetTokenValidationParameters(configuration);
    }

    public static TokenValidationParameters GetTokenValidationParameters(IConfiguration configuration)
    {

        SymmetricSecurityKey securityKey = GetSecurityKey(configuration);

        return new TokenValidationParameters()
        {
            IssuerSigningKey = securityKey,
            ValidAudience = _audience,
            ValidIssuer = _issuer,
            ClockSkew = TimeSpan.Zero
        };
    }

    private static SymmetricSecurityKey GetSecurityKey(IConfiguration configuration)
    {
        string secretKey = Environment
    .GetEnvironmentVariable("JWT_SIGNATURE_KEY")!
    ?? configuration["Jwt:SignatureKey"]!;
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
    }

}