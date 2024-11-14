using BikeRentalAuthGateway.DTOs.In;
using BikeRentalAuthGateway.DTOs.Out;
using BikeRentalAuthGateway.DTOs.Out.Errors;
using BikeRentalAuthGateway.Entities;
using BikeRentalAuthGateway.Helpers;
using BikeRentalAuthGateway.Messaging;
using BikeRentalAuthGateway.Models;
using BikeRentalAuthGateway.Services;

namespace BikeRentalAuthGateway.UseCases;

public class SignInUseCase(UserService userService, TokenService tokenService, LogService logService)
{
    public async Task<Result<SignInResponse>> ExecuteAsync(SignInRequest request)
    {
        User? savedUser = await userService.GetByEmailAsync(request.Email);

        var errorResult = Result.ForError<SignInResponse>(new IncorrrectUserOrPasswordErrorModel());

        if (savedUser is null)
        {
            await logService.AddLog(LogEvent.Warning("Login failed: User not found"));

            return errorResult;
        }

        if (CryptoHelper.VerifyHash(savedUser.HashPassword, request.Password))
        {

            SignInResponse response = CreateSignInResponse(savedUser);
            await logService.AddLog(LogEvent.Info("User logged successfully", null, savedUser.Id));
            return Result.Success(response);
        }
        else
        {
            await logService.AddLog(LogEvent.Warning("Login failed: Incorrect credentials", null, savedUser.Id));

            return errorResult;
        }

    }

    private SignInResponse CreateSignInResponse(User user)
    {
        string accessToken = tokenService.GenerateAccessToken(user);
        string refreshToken = tokenService.GenerateRefreshToken(user, accessToken);

        return new SignInResponse(user.Id, accessToken, refreshToken);
    }
}
