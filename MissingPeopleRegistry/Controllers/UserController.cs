using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MissingPeopleRegistry.Models;
using MissingPeopleRegistry.ViewModels.User;
using System.Data;
using System.Security.Claims;

namespace MissingPeopleRegistry.Controllers
{
    [Authorize(Roles = $"{nameof(Role.Admin)}")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var users = await _userManager.Users.Where(x =>x.Id != currentUserId).ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            await _userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ChangeUserStatus(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            user.Blocked = !user.Blocked;
            await _userManager.UpdateAsync(user);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var userModel = new UserVM { Email = user.Email, Id = id, LastName = user.LastName, Name = user.Name };
            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            var model = new UserUpdateVM { User = userModel, Role = role };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserUpdateVM request)
        {
            var user = await _userManager.FindByIdAsync(request.User.Id.ToString());
            user.Name = request.User.Name;
            user.LastName = request.User.LastName;
            user.Email = request.User.Email;
            await _userManager.UpdateAsync(user);
            
            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles.ToArray());
            await _userManager.AddToRoleAsync(user, request.Role);

            return RedirectToAction("Index");
        }
    }
}
