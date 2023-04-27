using Microsoft.AspNetCore.Mvc;
using MissingPeopleRegistry.Services;

namespace MissingPeopleRegistry.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMissingPeopleService _missingPeopleService;

        public HomeController(IMissingPeopleService missingPeopleService)
        {
            _missingPeopleService = missingPeopleService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _missingPeopleService.GetAllMissingPeople();
            return View(model);
        }
    }
}