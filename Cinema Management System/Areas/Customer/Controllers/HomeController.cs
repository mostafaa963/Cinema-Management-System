using Cinema_Management_System.DataAccess;
using Cinema_Management_System.Models;
using Cinema_Management_System.Repositories.IRepositories;
using Cinema_Management_System.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Transport.NamedPipes;

namespace Cinema_Management_System.Areas.Customer.Controllers
{
    [Area(SD.CUSTOMER_AREA)]
    [Authorize]
    //[Authorize (Roles = $"{RoleNames.CUSTOMER}")]
    public class HomeController : Controller
    {
        private readonly IRepository<Movie> _movies;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(IRepository<Movie> movies, UserManager<ApplicationUser> userManager)
        {
            _movies = movies;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var movies = await _movies.GetOneAsync(e => e.Id == id);
            if (movies == null)
                return NotFound();

                return View(movies);
        }
        public async Task<IActionResult> IndexAsync()
        {
            var movies = await _movies.GetAsync(includes: [e=>e.Home]);
            
            return View(movies);
        }
    }
}
