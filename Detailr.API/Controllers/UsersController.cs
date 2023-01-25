using Detailr.MVC.Contracts;
using Detailr.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace Detailr.API.Controllers
{
    public class UsersController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public UsersController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public IActionResult Login(string returnUrl = null) 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                returnUrl ??= Url.Content("~/");

                var isLoggedIn = await _authenticationService.Authenticate(login.Email, login.Password);
                if (isLoggedIn) 
                    return LocalRedirect(returnUrl);
            }
            ModelState.AddModelError("", "Login attempt failed. Please try again.");
            return View(login);
        }
    }
}
