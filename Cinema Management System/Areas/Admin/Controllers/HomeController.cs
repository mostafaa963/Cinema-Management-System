using Cinema_Management_System.DataAccess;
using Cinema_Management_System.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Cinema_Management_System.Areas.Admin.Controllers
{
    [Area(SD.ADMIN_AREA)]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public HomeController()
        {
            _db = new ApplicationDbContext();
        }
        public IActionResult Index()
        {
            var CountActors = _db.Actors.AsEnumerable().Count();
            var CountCategories = _db.Categories.AsEnumerable().Count();
            var CountCinemas = _db.Cinemas.AsEnumerable().Count();
            var CountMovies = _db.Movies.AsEnumerable().Count();
            return View(new CountEntityVM
            {
                CountActors = CountActors,
                CountCategory = CountCategories,
                CountCinema = CountCinemas,
                CountMovies = CountMovies
            });
        }
    }
}
