using Cinema_Management_System.DataAccess;
using Cinema_Management_System.Models;
using Cinema_Management_System.Repositories;
using Cinema_Management_System.Repositories.IRepositories;
using Cinema_Management_System.Service;
using Cinema_Management_System.Utilities;
using Cinema_Management_System.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema_Management_System.Areas.Admin.Controllers
{
    [Area(SD.ADMIN_AREA)]
    [Authorize (Roles =$"{RoleNames.ADMIN},{RoleNames.CUSTOMER},{RoleNames.SUPER_ADMIN}")]
    public class ActorController : Controller
    {
        //private readonly ApplicationDbContext _db;
        private readonly IRepository<Actor> _actor;
        private readonly IRepository<Movie> _movie;
        private readonly ActorServics actorServics;
        public ActorController(IRepository<Actor> actor, IRepository<Movie> movie)
        {
            //_db = new ApplicationDbContext();
            actorServics = new ActorServics();
            //_actor = new Repository<Actor>();
            //_movie = new Repository<Movie>();
            _actor = actor;
            _movie = movie;
        }
        public async Task<IActionResult>  Index(int page = 1, string? query = null,CancellationToken _cancellationToken=default) 
        {
            //var actor = _db.Actors.AsEnumerable();
            var actor = await _actor.GetAsync(cancellationToken: _cancellationToken);
            if (query is not null)
            {
                ViewBag.Query = query;
                actor = await _actor.GetAsync(expression: e => e.Name.Contains(query.Trim()), cancellationToken: _cancellationToken);
                //actor = actor.Where(e => e.Name.Contains(query.Trim()));
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
        public  async Task<IActionResult> Create( CancellationToken cancellation)
        {
            //var movie = _db.Movies.AsQueryable();
            var movie = await _movie.GetAsync(cancellationToken: cancellation);
            return View( new CreateActorViewModel{
            Movies= await _movie.GetAsync(cancellationToken: cancellation),
            Actor=new Actor()
            });
        }
        [HttpPost]
        public async Task<IActionResult> Create(Actor? actor, IFormFile logo, CancellationToken cancellation)
        {
            if (!ModelState.IsValid)
                return View(new CreateActorViewModel
                {
                    Movies = await _movie.GetAsync(cancellationToken: cancellation),
                    Actor = actor

                });
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
                //_db.Actors.Add(actor);
                await _actor.CreateAsync(actor,cancellationToken: cancellation);
                await _actor.CommitAsync(cancellationToken: cancellation);
                
            }
            TempData["success_notification"] = "Add Actor a successfully";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id, CancellationToken cancellationToken)
        {
            //var actor = _db.Actors.FirstOrDefault(e => e.ID == id);
            var actor = (await _actor.GetAsync(cancellationToken : cancellationToken)).FirstOrDefault(e => e.ID == id);
            return View(actor);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Actor actor, IFormFile? logo,CancellationToken cancellation)
        {
            if (!ModelState.IsValid)
                return View(actor);
            var actorDb = (await _actor.GetAsync(cancellationToken: cancellation,tracked:false)).SingleOrDefault(e => e.ID == actor.ID);

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
                _actor.Update(actor);
                await _actor.CommitAsync(cancellationToken: cancellation);
            }
            TempData["success_notification"] = "Update Actor a successfully";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteAsync(int id,CancellationToken cancellation)
        {
            var actor = (await _actor.GetAsync(cancellationToken: cancellation, tracked: false)).SingleOrDefault(e => e.ID == id);

            if (actor is null) return NotFound();
            if (actorServics.RemoveImg(actor.Image))
            {
                _actor.Delete(actor);
                await _actor.CommitAsync(cancellationToken: cancellation);

                TempData["success_notification"] = "Delete Actor Successfully";
            }
            else
                TempData["Error_notification"] = "Error: \"Cant Deleted this Actor\"";

            return RedirectToAction(nameof(Index));
        }
    }
}
