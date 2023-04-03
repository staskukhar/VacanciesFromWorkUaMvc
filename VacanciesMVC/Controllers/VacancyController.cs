using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TasteWork;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using VacanciesMVC.Models;
using VacanciesMVC.Services;

namespace VacanciesMVC.Controllers
{
    public class VacancyController : Controller
    {
        MyDbContext db;
        public VacancyController(MyDbContext dependencyInjection)
        {
            db = dependencyInjection;
        }

        public IActionResult Category()
        {
            IEnumerable<string> category = db.Vacancy
                .Select(v => v.TypeVacancy)
                .Distinct();
            return View(category);
        }
        public async Task<IActionResult> Index(string type, int page = 1)
        {
            var filteredVacancy = db.Vacancy.Where(v => v.TypeVacancy == type);

            ViewData["type"] = type;
            return View(await Pagination<Vacancy>.CreateAsync(filteredVacancy, page, 10));
        }
    }
}
