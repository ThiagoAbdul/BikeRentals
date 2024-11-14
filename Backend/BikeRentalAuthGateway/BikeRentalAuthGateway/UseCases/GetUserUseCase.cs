using BikeRentalAuthGateway.DTOs.Out;
using BikeRentalAuthGateway.DTOs.Out.Errors;
using BikeRentalAuthGateway.Entities;
using BikeRentalAuthGateway.Models;
using BikeRentalAuthGateway.Services;

namespace BikeRentalAuthGateway.UseCases;

public class GetUserUseCase(UserService userService)
{
    public async Task<Result<UserResponse>> ExecuteAsync(Guid id)
    {
        User? user = await userService.GetByIdAsync(id);

        if (user is null)
            return Result
                .ForError<UserResponse>(new ErrorModel("User not found"));

        UserResponse response = new (user);

        return Result.Success(response);

    }
}
