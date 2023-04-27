using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MissingPeopleRegistry.Models;
using MissingPeopleRegistry.ViewModels.Account;
using System.Data;

namespace MissingPeopleRegistry.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "Użytkownik o takim adresie e-mail już istnieje");
                return View(request);
            }

            var user = new User
            {
                Name = request.Name,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email
            };

            var registeredUser = await _userManager.CreateAsync(user, request.Password);
            if (registeredUser.Succeeded)
            {
                var usersCount = _userManager.Users.Count();

                if (usersCount == 1)
                {
                    await _roleManager.CreateAsync(new IdentityRole<Guid>(Role.User.ToString()));
                    await _roleManager.CreateAsync(new IdentityRole<Guid>(Role.Admin.ToString()));
                    await _userManager.AddToRoleAsync(user, Role.Admin.ToString());
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, Role.User.ToString());
                }
                return RedirectToAction(nameof(Login));
            }

            return View(request);
        }

        public IActionResult Login()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser == null)
            {
                ModelState.AddModelError("Email", "Użytkownik o takim adresie e-mail nie istnieje");
                return View(request);
            }

            if(existingUser.Blocked)
            {
                ViewBag.Message = "Twoje konto jest zablokowane. Skontaktuj się z administratorem strony.";
                return View(request);
            }

            if (await _userManager.CheckPasswordAsync(existingUser, request.Password))
            {
                var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, true, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("Password", "Niepoprawne hasło");
            return View(request);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}