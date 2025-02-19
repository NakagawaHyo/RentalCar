using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RentalCar.Models;
using RentalCar.Services.Auth;
using RentalCar.ViewModels;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace RentalCar.Controllers
{
    public class AuthController : Controller
    {
        private readonly RolesService _rolesService;
        private readonly UsersService _usersService;
        private readonly AuthService _authService;
        private readonly IConfiguration _configuration;

        public AuthController(UsersService usersService, RolesService rolesService, IConfiguration configuration, AuthService authService)
        {
            _usersService = usersService;
            _rolesService = rolesService;
            _configuration = configuration;
            _authService = authService;
        }

        [HttpGet, AllowAnonymous]
        [Route("/")]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            Login model = new Login();
            return View(model);
        }

        [HttpPost, AllowAnonymous]
        [Route("/")]
        public async Task<IActionResult> Login(Login model)
        {
            if (!ModelState.IsValid)
                return View();

            if (model == null)
            {
                return BadRequest("user is not set.");
            }

            var user = await _usersService.FindUserAsync(model.LoginId, model.Password);

            if (user == null)
            {
                ModelState.AddModelError("ログイン失敗", "ユーザー名またはパスワードが間違っています");
                return View();
            }

            var cookieClaims = await _authService.CreateCookieClaims(user);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                cookieClaims,
                new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe,
                    IssuedUtc = DateTimeOffset.Now,
                    ExpiresUtc = DateTimeOffset.Now.AddDays(1)
                });

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            if (HttpContext.User != null)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            return RedirectToAction("Login", "Auth");
        }
    }
}
