using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tourism.DataAccess;
using Tourism.Models;

namespace Tourism.Controllers
{
    public class CitiesController : Controller
    {
        private readonly TourismContext _context;

        public CitiesController(TourismContext context)
        {
            _context = context;
        }

        [Route("States/{stateId:int}/cities")]
        public IActionResult Index(int stateId)
        {
            var state = _context.States
                .Include(s => s.Cities)
                .Where(s => s.Id == stateId)
                .First();

            var cities = state.Cities;

            ViewData["StateName"] = state.Name;
            ViewData["StateId"] = state.Id;
            return View(cities);
        }

        [Route("States/{stateId:int}/cities/new")]
        public IActionResult New(int stateId)
        {
            var state = _context.States
                .Include(s => s.Cities)
                .Where(s => s.Id == stateId)
                .First();

            ViewData["StateName"] = state.Name;
            ViewData["StateId"] = state.Id;
            return View();
        }

        [HttpPost]
        [Route("/states/{stateId:int}/cities")]
        public IActionResult Create(int stateId, City city)
        {
            var state = _context.States
                .Include(s => s.Cities)
                .Where(s => s.Id == stateId)
                .First();
            state.Cities.Add(city);
            _context.SaveChanges();

            return RedirectToAction("index", new { stateId = state.Id });
        }
    }
}
