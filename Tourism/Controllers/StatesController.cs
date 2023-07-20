using Microsoft.AspNetCore.Mvc;
using Tourism.DataAccess;
using Tourism.Models;

namespace Tourism.Controllers
{
    public class StatesController : Controller
    {
        private readonly TourismContext _context;

        public StatesController(TourismContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var states = _context.States.ToList();
            return View(states);
        }

        public IActionResult New()
        {
            return View();
        }

        [Route("states/{stateId:int}")]
        public IActionResult Show(int stateId)
        {
            var state = _context.States.Find(stateId);
            return View(state);
        }

        [HttpPost]
        public IActionResult Index(State state)
        {
            _context.States.Add(state);
            _context.SaveChanges();

            var newStateId = state.Id;

            return RedirectToAction("show", new { id = newStateId });
        }
    }
}
