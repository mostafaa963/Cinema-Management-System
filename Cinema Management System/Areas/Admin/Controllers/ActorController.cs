using Cinema_Management_System.DataAccess;
using Cinema_Management_System.Models;
using Cinema_Management_System.Service;
using Cinema_Management_System.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema_Management_System.Areas.Admin.Controllers
{
    [Area(SD.ADMIN_AREA)]
    public class ActorController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ActorServics actorServics;
        public ActorController()
        {
            _db = new ApplicationDbContext();
            actorServics = new ActorServics();
        }
        public IActionResult Index(int page = 1, string? query = null)
        {
            var actor = _db.Actors.AsEnumerable();
            if (query is not null)
            {
                ViewBag.Query = query;
                actor = actor.Where(e => e.Name.Contains(query.Trim()));
            }
            int totalPages = (int)Math.Ceiling(actor.Count() / 5.0);
            actor = actor.Skip((page - 1) * 5).Take(5);
            return View(new ActorWithRelatedVM
            {
                Actors = actor,
                totalPages = totalPages,
                CurrentPage = page
            });
        }
        [HttpGet]
        public IActionResult Create()
        {
            var movie = _db.Movies.AsQueryable();
            return View(movie.AsEnumerable());
        }
        [HttpPost]
        public IActionResult Create(Actor? actor, IFormFile logo)
        {
            if (logo is not null && logo.Length > 0)
            {

                var fileName = actorServics.SaveImg(logo);

                if (fileName is not null)
                {

                    actor.Image = fileName;
                }
            }
            if (actor == null)
                return NotFound();
            if (actor is not null)
            {
                _db.Actors.Add(actor);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var actor = _db.Actors.FirstOrDefault(e => e.ID == id);
            return View(actor);
        }
        [HttpPost]
        public IActionResult Update(Actor actor, IFormFile? logo)
        {
            var actorDb = _db.Actors.AsNoTracking().SingleOrDefault(e => e.ID == actor.ID);

            if (actorDb is null) return NotFound();

            if (logo is not null && logo.Length > 0)
            {
                var fileName = actorServics.SaveImg(logo);

               
                actorServics.RemoveImg(actorDb.Image);

                
                if (fileName is not null) actor.Image = fileName;
            }
            else
                actor.Image = actorDb.Image;
            ;
            if (actor == null)
                return NotFound();
            if (actor is not null)
            {
                _db.Actors.Update(actor);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var actor = _db.Actors.SingleOrDefault(e => e.ID == id);

            if (actor is null) return NotFound();
            if (actorServics.RemoveImg(actor.Image))
            {
                _db.Actors.Remove(actor);
                _db.SaveChanges();

                TempData["success_notification"] = "Delete Actor Successfully";
            }
            else
                TempData["Error_notification"] = "Error: \"Cant Deleted this Actor\"";

            return RedirectToAction(nameof(Index));
        }
    }
}
