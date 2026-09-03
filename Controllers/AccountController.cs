using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MyPasswords.Models;
using MyPasswords.Services.Account;
using MyPasswords.Services.Register;
using System.Security.Claims;

namespace MyPasswords.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, [FromServices] ILoginService loginService)
        {
            if (!ModelState.IsValid) return View(model);

            var loggedUser = await loginService.Login(model);

            if (loggedUser is null)
            {
                ModelState.AddModelError(string.Empty, "Email or Password is invalid");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, loggedUser.Id.ToString()),
                new Claim(ClaimTypes.Name, loggedUser.UserName)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, [FromServices] IRegisterServices registerService)
        {
            if (!ModelState.IsValid) return View(model);
            var isRegistered = await registerService.Register(model);
            if (!isRegistered)
            {
                ModelState.AddModelError(string.Empty, "Email already exists");
                return View(model);
            }
            return RedirectToAction("Login");
        }

    }
}
