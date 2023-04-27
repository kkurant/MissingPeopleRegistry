using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MissingPeopleRegistry.Models;
using MissingPeopleRegistry.Services;
using MissingPeopleRegistry.ViewModels.MissingPeople;
using System.Security.Claims;

namespace MissingPeopleRegistry.Controllers
{
    [Authorize]
    public class MissingPeopleController : Controller
    {
        private readonly IMissingPeopleService _missingPeopleService;
        public MissingPeopleController(IMissingPeopleService missingPeopleService) 
        { 
            _missingPeopleService = missingPeopleService;
        }

        public IActionResult CreateMissingPerson()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateMissingPerson(MissingPersonVM request)
        {
            if (ModelState.IsValid)
            {
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                await _missingPeopleService.CreateAsync(request, userId);
                return RedirectToAction("Index", "Home");
            }

            return View(request);
        }

        [AllowAnonymous]
        public async Task<IActionResult> GetMissingPerson(Guid id)
        {
            var model = await _missingPeopleService.GetMissingPersonById(id);
            return View(model);
        }

        [Authorize(Roles = $"{nameof(Role.Admin)}")]
        public async Task<IActionResult> DeleteMissingPerson(Guid id)
        {
            await _missingPeopleService.DeleteMissingPersonById(id);
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = $"{nameof(Role.Admin)}")]
        public async Task<IActionResult> UpdateMissingPerson(Guid id)
        {
            var model = await _missingPeopleService.GetMissingPersonById(id);
            return View(model);
        }

        [Authorize(Roles = $"{nameof(Role.Admin)}")]
        [HttpPost]
        public async Task<IActionResult> UpdateMissingPerson(MissingPersonVM request)
        {
            if (ModelState.IsValid)
            {
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                await _missingPeopleService.UpdateAsync(request, userId);
                return RedirectToAction("Index", "Home");
            }

            return View(request);
        }
    }
}