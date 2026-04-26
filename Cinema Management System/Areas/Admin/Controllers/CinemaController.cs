using Cinema_Management_System.DataAccess;
using Cinema_Management_System.Models;
using Cinema_Management_System.Service;
using Cinema_Management_System.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema_Management_System.Areas.Admin.Controllers
{
    [Area(SD.ADMIN_AREA)]
    public class CinemaController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly CinemaServices cinemaServics;
        public CinemaController()
        {
            _db = new ApplicationDbContext();
            cinemaServics=new CinemaServices();
        }
        public IActionResult Index(int page = 1)
        {
            var cinema = _db.Cinemas.AsEnumerable();
            int totalPages = (int)Math.Ceiling(cinema.Count() / 5.0);
            cinema = cinema.Skip((page - 1) * 5).Take(5);
            return View(new CinemaWithRelatedVM
            {
                Cinemas = cinema,
                totalPages = totalPages,
                CurrentPage=page
            });
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Cinema? cinema, IFormFile logo)
        {
            if (logo is not null && logo.Length > 0)
            {
                
                var fileName = cinemaServics.SaveImg(logo);

                if (fileName is not null)
                {
                    
                    cinema.Logo = fileName;
                }
            }
            if (cinema == null)
                return NotFound();
            if (cinema is not null)
            {
                _db.Cinemas.Add(cinema);
                _db.SaveChanges();
            }
            return RedirectToAction (nameof(Index));
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var cinema= _db.Cinemas.FirstOrDefault(e=>e.ID==id);
            return View(cinema);
        }
        [HttpPost]
        public IActionResult Update(Cinema cinema, IFormFile? logo)
        {
            var cinemaDb = _db.Cinemas.AsNoTracking().SingleOrDefault(e => e.ID == cinema.ID);

            if (cinemaDb is null) return NotFound();

            if (logo is not null && logo.Length > 0)
            {
               
                var fileName = cinemaServics.SaveImg(logo);

                cinemaServics.RemoveImg(cinemaDb.Logo);

                
                if (fileName is not null) cinema.Logo = fileName;
            }
            else
                cinema.Logo = cinemaDb.Logo;
            
            if (cinema is not null)
            {
                _db.Cinemas.Update(cinema);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var cinema = _db.Cinemas.SingleOrDefault(e => e.ID == id);

            if (cinema is null) return NotFound();

            _db.Cinemas.Remove(cinema);
            _db.SaveChanges();


            return RedirectToAction(nameof(Index));
        }
    }
}
