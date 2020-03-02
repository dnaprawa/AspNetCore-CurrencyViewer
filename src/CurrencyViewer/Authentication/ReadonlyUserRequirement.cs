using Microsoft.AspNetCore.Authorization;

namespace CurrencyViewer.API.Authentication
{
    public class ReadonlyUserRequirement : IAuthorizationRequirement
    {
    }
}
