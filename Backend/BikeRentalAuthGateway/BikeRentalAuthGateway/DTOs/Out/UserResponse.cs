using BikeRentalAuthGateway.Entities;

namespace BikeRentalAuthGateway.DTOs.Out;

public class UserResponse(User user)
{
    public Guid Id { get; set; } = user.Id;
    public string FullName { get; set; } = user.FullName;
    public string Email { get; set; } = user.Email;
    public bool AccountEnabled { get; set; } = user.AccountEnabled;
    public DateTime CreatedAt { get; set; } = user.CreatedAt;
}
