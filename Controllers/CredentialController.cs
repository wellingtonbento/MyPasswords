using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPasswords.Models;
using MyPasswords.Services.Credential.Register;

namespace MyPasswords.Controllers
{
    [Authorize]
    public class CredentialController : Controller
    {
        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult Create() => View(new RegisterCredentialViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterCredentialViewModel model,
                                                [FromServices] IRegisterCredentialServices registerCredentialServices)
        {
            if (!ModelState.IsValid) return View(model);

            var isRegistered = await registerCredentialServices.Register(model);
            if (!isRegistered)
            {
                ModelState.AddModelError(string.Empty, "Could not create the credential");
                return View(model);
            }

            return RedirectToAction("Index");
        }
    }
}