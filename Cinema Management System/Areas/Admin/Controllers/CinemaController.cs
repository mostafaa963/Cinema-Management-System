using Cinema_Management_System.DataAccess;
using Cinema_Management_System.Models;
using Cinema_Management_System.Repositories;
using Cinema_Management_System.Repositories.IRepositories;
using Cinema_Management_System.Service;
using Cinema_Management_System.Utilities;
using Cinema_Management_System.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Cinema_Management_System.Areas.Admin.Controllers
{
    [Area(SD.ADMIN_AREA)]
    public class CinemaController : Controller
    {
        //private readonly ApplicationDbContext _db;
        private readonly IRepository<Cinema> _cinema;
        private readonly CinemaServices cinemaServics;
        public CinemaController(IRepository<Cinema> cinema)
        {
            //_db = new ApplicationDbContext();
            cinemaServics = new CinemaServices();
            _cinema = cinema;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            //var cinema = _db.Cinemas.AsEnumerable();
            var cinema = await _cinema.GetAsync();
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
            return View(new Cinema());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Cinema? cinema, IFormFile logo,CancellationToken cancellation)
        {
            if (!ModelState.IsValid)
                return View(cinema);
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
              await _cinema.CreateAsync(cinema,cancellationToken: cancellation);
                await _cinema.CommitAsync(cancellationToken: cancellation);
            }
            TempData["success_notification"] = "Add Cinema Successfully";
            return RedirectToAction (nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id,CancellationToken cancellation)
        {
            //var cinema= _db.Cinemas.FirstOrDefault(e=>e.ID==id);
            var cinema= (await _cinema.GetAsync(cancellationToken: cancellation)) .FirstOrDefault(e=>e.ID==id);
            return View(cinema);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Cinema cinema, IFormFile? logo ,CancellationToken cancellation )
        {
            if (!ModelState.IsValid)
                return View(cinema);
            var cinemaDb =(await _cinema.GetAsync(tracked:false,cancellationToken: cancellation)).SingleOrDefault(e => e.ID == cinema.ID);

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
               _cinema.Update(cinema);
              await  _cinema.CommitAsync(cancellationToken: cancellation);
            }
            TempData["success_notification"] = "Update Cinema Successfully";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id,CancellationToken cancellation)
        {
            var cinema = ( await _cinema.GetAsync()).SingleOrDefault(e => e.ID == id);

            if (cinema is null) return NotFound();

            _cinema.Delete(cinema);
           await _cinema.CommitAsync(cancellationToken: cancellation);


            TempData["success_notification"] = "Delete Cinema Successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
