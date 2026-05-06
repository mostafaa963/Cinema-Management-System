using Cinema_Management_System.DataAccess;
using Cinema_Management_System.Models;
using Cinema_Management_System.Repositories;
using Cinema_Management_System.Repositories.IRepositories;
using Cinema_Management_System.Service;
using Cinema_Management_System.Utilities;
using Cinema_Management_System.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Numerics;

namespace Cinema_Management_System.Areas.Admin.Controllers
{
    [Area(SD.ADMIN_AREA)]
    public class MovieController : Controller
    {
        //private readonly ApplicationDbContext _db;
        private readonly IRepository<Movie> _Movies;
        private readonly IRepository<Cinema> _cinema;
        private readonly IRepository<Actor> _actor;
        private readonly IRepository<Category> _category;
        private readonly IRepositorySubImage subImage;
        private readonly MoviesServices moviesServices;
        //public MovieController()
        //{
        //    _cinema=new Repository<Cinema>();
        //    _actor=new Repository<Actor>();
        //    _category=new  Repository<Category>();
        //    subImage = new RepositorySubImage();
        //    _Movies = new Repository<Movie>();
        //    //_db = new ApplicationDbContext();
        //}

        public MovieController(IRepository<Movie> movies, IRepository<Cinema> cinema, IRepository<Actor> actor, IRepository<Category> category, IRepositorySubImage subImage)
        {
            _Movies = movies;
            _cinema = cinema;
            _actor = actor;
            _category = category;
            this.subImage = subImage;
            moviesServices = new MoviesServices();
        }

        //public MovieController(Repository<Cinema> cinema, Repository<Actor> actor, Repository<Category> category)
        //{
        //    _cinema = cinema;
        //    _actor = actor;
        //    _category = category;
        //}

        public async Task<IActionResult> Index(int Page = 1, string? query = null, int sort = 0)
        {
            var movie = await _Movies.GetAsync() ;
            int  numberEntityPerPage = 5;
            int   countPage =(int)Math.Ceiling (((double) movie.Count()) / numberEntityPerPage);
            if (query != null)
            {
                movie = movie.Where(x => x.Name.Contains(query.Trim()));
                ViewBag.Query = query;
            }


            movie = movie.Skip((Page - 1) * numberEntityPerPage).Take(numberEntityPerPage);
            if (sort == 1)
                movie = movie.OrderByDescending(e => e.Id);
            if (sort == 2)
                movie = movie.OrderBy(e => e.Price);
            return View(new MoviesIndexVM
            {
                Movies = movie,
                TotalPage = countPage,
                CurrentPage = Page
            });
        }
        [HttpGet]
        public async Task<IActionResult> Create( CancellationToken cancellation)
        {
            return View(new CMoviesVM
            {
                Cinemas = await _cinema.GetAsync(cancellationToken: cancellation),
                Categories = await _category.GetAsync(cancellationToken: cancellation),
                Actors = await _actor.GetAsync(cancellationToken: cancellation),
                Name =null,
                Price=0,
                Date= DateTime.Now,
                Description=null,
            });
        }
        [HttpPost]
        public async Task<IActionResult> Create(MovieCreateVM? movie, IFormFile? MainImg, List<IFormFile>? SubImg,CancellationToken cancellation)
        {
            var _movies = new Movie();
            if (!ModelState.IsValid)
                return View(new CMoviesVM
                {
                    Cinemas = await _cinema.GetAsync(cancellationToken: cancellation),
                    Categories = await _category.GetAsync(cancellationToken: cancellation),
                    Actors = await _actor.GetAsync(cancellationToken: cancellation),
                    Name = movie.Name,
                    Price = movie.Price,
                    Date = movie.Date,
                    Description = movie.Description,

                });
            if (MainImg != null && MainImg.Length > 0)
            {
                string? fileName = moviesServices.SaveImg(MainImg, TypeImage.MainImage);
                if (fileName != null)
                    _movies.MainImg = fileName;
            }

            if (movie != null)
            {
                _movies.Name = movie.Name;
                _movies.Price = movie.Price;
                _movies.Status = movie.Status;
                _movies.Date = movie.Date;
                _movies.Description = movie.Description;
                _movies.CinemaID = movie.CinemaID;
                //Category  
                _movies.HomeId = movie.CategoryId;
              await  _Movies.CreateAsync(_movies,cancellationToken:cancellation);
                await _Movies.CommitAsync(cancellationToken: cancellation);
            }
            foreach (var image in SubImg)
            {
                if (image != null && image.Length > 0)
                {
                    string? fileName = moviesServices.SaveImg(image, TypeImage.SubImage);
                    if (fileName != null)
                    {
                      await subImage.CreateAsync(new MoviesSubimage
                        {
                            MovieID = _movies.Id,
                            SubImage = fileName
                        });
                     await _category.CommitAsync(cancellationToken:cancellation);
                    }

                }
            }
            TempData["success_notification"] = "Add Movie Successfully";

            return RedirectToAction(nameof(Index));
        }
        //[HttpGet]
        //public IActionResult Update(int id)
        //{
        //    var _movie = _db.Movies.FirstOrDefault(a => a.Id == id);
        //    if (_movie is null)
        //        return NotFound();

        //    return View(new UpdateMoviesVM
        //    {
        //        movie = _movie,
        //        Categories = _db.Categories.AsEnumerable(),
        //        Actors = _db.Actors.AsEnumerable(),
        //        Cinemas = _db.Cinemas.AsEnumerable()
        //    });
        //}
        [HttpGet]
        public async Task<IActionResult> Update(int id,CancellationToken cancellation)
        {
            var _movie = (await _Movies.GetAsync(cancellationToken: cancellation)).FirstOrDefault(a => a.Id == id);
            if (_movie is null)
                return NotFound();

            return View(new UpdateMoviesVM
            {
                movie = _movie,
                Cinemas = await _cinema.GetAsync(cancellationToken: cancellation),
                Categories = await _category.GetAsync(cancellationToken: cancellation),
                Actors = await _actor.GetAsync(cancellationToken: cancellation),
            });
        }
        
        [HttpPost]
        public async Task<IActionResult> Update(UpdateInsetMoviesVM movieVM, IFormFile? MainImg, List<IFormFile>? SubImg,CancellationToken cancellation )
        {
            
            var existingMovie = (await _Movies.GetAsync(cancellationToken: cancellation)).SingleOrDefault(e => e.Id == movieVM.Id);
            if (existingMovie == null) return NotFound();
            if (!ModelState.IsValid)
                return View(new UpdateMoviesVM
                {
                    Cinemas = await _cinema.GetAsync(cancellationToken: cancellation),
                    Categories = await _category.GetAsync(cancellationToken: cancellation),
                    Actors = await _actor.GetAsync(cancellationToken: cancellation),
                    movie = new Movie
                    {
                        Id = movieVM.Id,
                        Name = movieVM.Name,
                        Description = movieVM.Description,
                        Price = movieVM.Price,
                        Status = movieVM.Status,
                        Date = movieVM.Date,
                        CinemaID = movieVM.CinemaID,
                        HomeId = movieVM.CategoryId
                    }
                });

            if (MainImg != null && MainImg.Length > 0)
            {
                string? fileName = moviesServices.SaveImg(MainImg, TypeImage.MainImage);
                if (fileName != null)
                {
                    moviesServices.RemoveImg(existingMovie.MainImg, TypeImage.MainImage);
                    existingMovie.MainImg = fileName;
                }
            }

            existingMovie.Name = movieVM.Name;
            existingMovie.Price = movieVM.Price;
            existingMovie.Status = movieVM.Status; 
            existingMovie.Date = movieVM.Date;
            existingMovie.Description = movieVM.Description;
            existingMovie.CinemaID = movieVM.CinemaID;
            existingMovie.HomeId = movieVM.CategoryId;
            _Movies.Update(existingMovie);
             await _Movies.CommitAsync(cancellationToken: cancellation);
            if (SubImg != null && SubImg.Count > 0)
            {
                
                var oldSubImages = (await subImage.GetAsync(cancellationToken:cancellation)).Where(e => e.MovieID == existingMovie.Id).ToList();
                foreach (var img in oldSubImages)
                {
                    moviesServices.RemoveImg(img.SubImage, TypeImage.SubImage);
                }
                await subImage.RemoveRang(oldSubImages);

               
                foreach (var image in SubImg)
                {
                    if (image.Length > 0)
                    {
                        string? fileName = moviesServices.SaveImg(image, TypeImage.SubImage);
                        if (fileName != null)
                        {
                            await subImage.CreateAsync(new MoviesSubimage
                            {
                                MovieID = existingMovie.Id,
                                SubImage = fileName
                            },cancellationToken :cancellation);
                        }
                    }
                }
            }

            await subImage.CommitAsync(cancellationToken:cancellation); 
            TempData["success_notification"] = "Update Movie Successfully";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id,CancellationToken cancellation)
        {
            var movie =(await _Movies.GetAsync(cancellationToken: cancellation)).SingleOrDefault(e => e.Id == id);
            var oldSubImages =(await subImage.GetAsync(cancellationToken: cancellation)).Where(e => e.MovieID == movie.Id).ToList();
            foreach (var img in oldSubImages)
            {
                moviesServices.RemoveImg(img.SubImage, TypeImage.SubImage);
            }
            await subImage.RemoveRang(oldSubImages);

            if (movie is null) return NotFound();
            if (moviesServices.RemoveImg(movie.MainImg, TypeImage.MainImage))
            {
                _Movies.Delete(movie);
             await _Movies.CommitAsync();

            }
            
            TempData["success_notification"] = "Delete Movie Successfully";

            return RedirectToAction(nameof(Index));
        }
    }
}
