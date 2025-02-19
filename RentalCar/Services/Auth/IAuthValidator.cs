using Microsoft.AspNetCore.Authentication.Cookies;

namespace RentalCar.Services.Auth
{
    public interface IAuthValidator
    {
        Task ValidateAsync(CookieValidatePrincipalContext context);
    }
}
