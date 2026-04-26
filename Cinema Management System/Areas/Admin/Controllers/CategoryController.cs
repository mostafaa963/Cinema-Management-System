using Cinema_Management_System.DataAccess;
using Cinema_Management_System.Models;
using Cinema_Management_System.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema_Management_System.Areas.Admin.Controllers
{
    [Area(SD.ADMIN_AREA)]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController()
        {
            _db = new ApplicationDbContext();
        }
        public IActionResult Index(int page = 1)
        {
            var category = _db.Categories.AsEnumerable();
            int totalPages = (int)Math.Ceiling(category.Count() / 5.0);
            category = category.Skip((page - 1) * 5).Take(5);
            return View(new CategoryWithRelatedVM {
                categories = category,
                CurrentPage = page,
                totalPages = totalPages
            });
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category == null)
                return NotFound();
            if (category is not null)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
            }
            return RedirectToAction (nameof(Index));
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var category= _db.Categories.FirstOrDefault(e=>e.ID==id);
            return View(category);
        }
        [HttpPost]
        public IActionResult Update(Category category)
        {
            //var _categoryUpdate = _db.Categories.FirstOrDefault(e => e.ID == category.ID);
            if (category == null)
                return NotFound();
            if (category is not null)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var category = _db.Categories.SingleOrDefault(e => e.ID == id);

            if (category is null) return NotFound();

            _db.Categories.Remove(category);
            _db.SaveChanges();

            

            return RedirectToAction(nameof(Index));
        }
    }
}
