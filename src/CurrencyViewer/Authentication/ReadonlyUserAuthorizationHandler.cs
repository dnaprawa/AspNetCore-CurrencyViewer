using CurrencyViewer.Application.Security;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace CurrencyViewer.API.Authentication
{
    public class ReadonlyUserAuthorizationHandler : AuthorizationHandler<ReadonlyUserRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ReadonlyUserRequirement requirement)
        {
            if (context.User.IsInRole(Roles.Readonly))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
