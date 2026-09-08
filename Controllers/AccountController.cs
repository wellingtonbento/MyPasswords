using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPasswords.Models;
using MyPasswords.Repositories.Interfaces;
using MyPasswords.Services.ObtainUserLogged;
using MyPasswords.Services.User.Delete;
using MyPasswords.Services.User.Login;
using MyPasswords.Services.User.Register;
using MyPasswords.Services.User.Update;
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
                new Claim(ClaimTypes.Name, loggedUser.Name)
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Update([FromServices] ILoggedUser loggedUser,
                                      [FromServices] IUserRepository userRepository)
        {
            var user = await userRepository.GetById(loggedUser.Id);
            if (user is null) return RedirectToAction("Login");

            return View(new UserEditViewModel { Name = user.Name });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UserEditViewModel model, [FromServices] IUpdateServices updateServices)
        {
            if (!ModelState.IsValid) return View(model);

            var isUpdated = await updateServices.Update(model);
            if (!isUpdated)
            {
                ModelState.AddModelError(string.Empty, "Could not update the user");
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Delete() => View();

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromServices] IDeleteService deleteService)
        {
            var isDeleted = await deleteService.Delete();
            if (!isDeleted)
            {
                ModelState.AddModelError(string.Empty, "Could not delete the user");
                return View();
            }

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

    }
}
