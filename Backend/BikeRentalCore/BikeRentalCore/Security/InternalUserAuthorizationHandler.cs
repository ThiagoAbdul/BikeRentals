using Microsoft.AspNetCore.Authorization;

namespace BikeRentalCore.Security;

public class InternalUserAuthorizationHandler : AuthorizationHandler<InternalUserRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, InternalUserRequirement requirement)
    {
        if (context.User.HasClaim(c => c.Type == requirement.RequiredClaim))
        {
            context.Succeed(requirement);
        }
        // Fail
        return Task.CompletedTask;
    }
}
