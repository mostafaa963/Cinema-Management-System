using Cinema_Management_System.DataAccess;
using Cinema_Management_System.Models;
using Cinema_Management_System.Service;
using Cinema_Management_System.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Cinema_Management_System.Areas.Admin.Controllers
{
    [Area(SD.ADMIN_AREA)]
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly MoviesServices moviesServices;
        public MovieController()
        {
            _db = new ApplicationDbContext();
            moviesServices = new MoviesServices();
        }
        public IActionResult Index(int Page = 1, string? query = null, int sort = 0)
        {
            var movie = _db.Movies.AsQueryable();
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
        public IActionResult Create()
        {
            return View(new CMoviesVM
            {
                Cinemas = _db.Cinemas.AsEnumerable(),
                Categories = _db.Categories.AsEnumerable(),
                Actors = _db.Actors.AsEnumerable(),
            });
        }
        [HttpPost]
        public IActionResult Create(MovieCreateVM? movie, IFormFile? MainImg, List<IFormFile>? SubImg)
        {
            var _movies = new Movie();
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
                _db.Movies.Add(_movies);
                _db.SaveChanges();
            }
            foreach (var image in SubImg)
            {
                if (image != null && image.Length > 0)
                {
                    string? fileName = moviesServices.SaveImg(image, TypeImage.SubImage);
                    if (fileName != null)
                    {
                        _db.MoviesSubimages.Add(new MoviesSubimage
                        {
                            MovieID = _movies.Id,
                            SubImage = fileName
                        });
                        _db.SaveChanges();
                    }

                }
            }
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
        public IActionResult Update(int id)
        {
            var _movie = _db.Movies.FirstOrDefault(a => a.Id == id);
            if (_movie is null)
                return NotFound();

            return View(new UpdateMoviesVM
            {
                movie = _movie,
                Categories = _db.Categories.AsEnumerable(),
                Actors = _db.Actors.AsEnumerable(),
                Cinemas = _db.Cinemas.AsEnumerable()
            });
        }
        
        [HttpPost]
        public IActionResult Update(UpdateInsetMoviesVM movieVM, IFormFile? MainImg, List<IFormFile>? SubImg)
        {
            
            var existingMovie = _db.Movies.SingleOrDefault(e => e.Id == movieVM.Id);
            if (existingMovie == null) return NotFound();

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

            if (SubImg != null && SubImg.Count > 0)
            {
                
                var oldSubImages = _db.MoviesSubimages.Where(e => e.MovieID == existingMovie.Id).ToList();
                foreach (var img in oldSubImages)
                {
                    moviesServices.RemoveImg(img.SubImage, TypeImage.SubImage);
                }
                _db.MoviesSubimages.RemoveRange(oldSubImages);

               
                foreach (var image in SubImg)
                {
                    if (image.Length > 0)
                    {
                        string? fileName = moviesServices.SaveImg(image, TypeImage.SubImage);
                        if (fileName != null)
                        {
                            _db.MoviesSubimages.Add(new MoviesSubimage
                            {
                                MovieID = existingMovie.Id,
                                SubImage = fileName
                            });
                        }
                    }
                }
            }

            _db.SaveChanges(); 
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var movie = _db.Movies.SingleOrDefault(e => e.Id == id);
            var oldSubImages = _db.MoviesSubimages.Where(e => e.MovieID == movie.Id).ToList();
            foreach (var img in oldSubImages)
            {
                moviesServices.RemoveImg(img.SubImage, TypeImage.SubImage);
            }
            _db.MoviesSubimages.RemoveRange(oldSubImages);

            if (movie is null) return NotFound();
            if (moviesServices.RemoveImg(movie.MainImg, TypeImage.MainImage))
            {
                _db.Movies.Remove(movie);
                _db.SaveChanges();

            }
            

            return RedirectToAction(nameof(Index));
        }
    }
}
