using Microsoft.AspNetCore.Authorization;

namespace BikeRentalCore.Security;

public class InternalUserRequirement : IAuthorizationRequirement
{
    public string RequiredClaim { get => "Internal"; }

}
