using BikeRentalAuthGateway.DTOs.In;
using BikeRentalAuthGateway.DTOs.Out;
using BikeRentalAuthGateway.Data;
using BikeRentalAuthGateway.DTOs.Out.Errors;
using BikeRentalAuthGateway.Entities;
using BikeRentalAuthGateway.Helpers;
using BikeRentalAuthGateway.Models;
using BikeRentalAuthGateway.Services;
using Microsoft.EntityFrameworkCore;
using BikeRentalAuthGateway.Messaging;

namespace BikeRentalAuthGateway.UseCases;

public sealed class SignUpUseCase(UserService userService, LogService logService)
{
    public async Task<Result<SignUpResponse>> ExecuteAsync(SignUpRequest request)
    {
        User? savedUser = await userService.GetByEmailAsync(request.Email);

        if (savedUser is not null)
            return Result.ForError<SignUpResponse>(new EmailAlreadyRegisteredErrorModel());

        string hashPassword = CryptoHelper.GenerateHash(request.Password);

        User user = new(request.FullName, request.Email, hashPassword);

        await userService.CreateAsync(user);

        SignUpResponse response = new(user.Id);

        await logService.AddLog(LogEvent.Info($"User created: {user.Email}", null, user.Id));


        return Result.Success(response);
        
    }
}
